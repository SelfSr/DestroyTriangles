using Plugins.Audio.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using YG;

public class LevelManager : MonoBehaviour
{
    private const string TRIANGLE = "Triangle";

    [SerializeField] private GameObject[] levelObjectsArray;
    [SerializeField] private RestartLevel restartLevelScript;
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] LeaderboardYG leaderboardYG;

    [SerializeField] private Animator animatorPanel;
    [SerializeField] private SourceAudio source;

    public Dictionary<int, GameObject> levelObjects = new Dictionary<int, GameObject>();

    [HideInInspector] public static int currentLevel = 1;

    private bool isOneStartCoroutine = true;

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    private void Start()
    {
        for (int i = 0; i < levelObjectsArray.Length; i++)
            levelObjects.Add(i + 1, levelObjectsArray[i]);
        currentLevel = PlayerPrefs.GetInt("SelectedLevel", 1);
        SwitchObject();
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
                ShowGreenpanel();
                LevelController.instance.isEndGame();

                YandexGame.FullscreenShow();
                int lastScore = currentLevel;
                if (currentLevel >= lastScore)
                    YandexGame.NewLeaderboardScores(leaderboardYG.nameLB, currentLevel == 97 ? 96 : currentLevel);

                SwitchObject();
            }
            yield return null;
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
    private void Rewarded(int id)
    {
        SkiplevelButton();
    }
    private void SkiplevelButton()
    {
        currentLevel++;
        isOneStartCoroutine = true;
        restartLevelScript.ResetBall();
        LevelController.instance.isEndGame();
        YandexGame.NewLeaderboardScores(leaderboardYG.nameLB, currentLevel);
        SwitchObject();
    }
    private void ShowGreenpanel()
    {
        source.Play("LevelComplete");
        animatorPanel.Play("Win");
    }
}
