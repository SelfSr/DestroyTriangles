using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private const float DELAY_COROUTINE = 0.1f;
    private const string TRIANGLE = "Triangle";

    [SerializeField] private GameObject[] levelObjectsArray;
    [SerializeField] private RestartLevel restartLevelScript;
    [SerializeField] private TextMeshProUGUI levelNumber;
    public Dictionary<int, GameObject> levelObjects = new Dictionary<int, GameObject>();

    [HideInInspector] public int currentLevel = 1;
    private bool isOneStartCoroutine = true;

    private void Start()
    {
        for (int i = 0; i < levelObjectsArray.Length; i++)
            levelObjects.Add(i + 1, levelObjectsArray[i]);
        currentLevel = PlayerPrefs.GetInt("SelectedLevel", 1);
        SwitchObject();
        //ActivateLevelObjects(selectedLevel);

    }
    private IEnumerator CheckForTrianglesWithDelay(GameObject parentObject)
    {
        while (true)
        {
            bool foundTriangle = false;
            foreach (Transform child in parentObject.transform)
            {
                if (child.CompareTag(TRIANGLE) && child.gameObject.activeSelf)
                    foundTriangle = true;
            }
            if (!foundTriangle)
            {
                currentLevel++;
                isOneStartCoroutine = true;
                restartLevelScript.ResetBall();
                SwitchObject();
            }
            yield return new WaitForSeconds(DELAY_COROUTINE);
        }
    }
    private void SwitchObject()
    {
        DeactivateCurrentObject();
        if (levelObjects.ContainsKey(currentLevel))
        {
            ActivateObject(levelObjects[currentLevel]);
            levelNumber.text = currentLevel.ToString();
            if (isOneStartCoroutine)
            {
                StopAllCoroutines();
                StartCoroutine(CheckForTrianglesWithDelay(levelObjects[currentLevel]));
                isOneStartCoroutine = false;
            }
        }
        else
        {
            Debug.LogWarning("Level " + currentLevel + " not found.");
        }
    }
    private void ActivateObject(GameObject obj)
    {
        obj.SetActive(true);
    }
    private void DeactivateCurrentObject()
    {
        int previousLevel = currentLevel - 1;
        if (levelObjects.ContainsKey(previousLevel))
            levelObjects[previousLevel].SetActive(false);
    }
    //private void ActivateLevelObjects(int levelIndex)
    //{
    //    foreach (GameObject obj in levelObjectsArray)
    //    {
    //        obj.SetActive(false);
    //    }
    //    if (levelIndex >= 0 && levelIndex < levelObjectsArray.Length)
    //    {
    //        levelObjects[levelIndex].SetActive(true);
    //    }
    //    SwitchObject();
    //}
}
