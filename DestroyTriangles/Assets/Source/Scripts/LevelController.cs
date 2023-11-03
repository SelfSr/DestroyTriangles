using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private const string LEVELINDEX = "LevelIndex";
    private const string TRIANGLE = "Triangle";
    public static int LEVELCOUNT = 7;

    public static LevelController instance = null;
    [SerializeField] private LevelManager levelManager;
    int levelComplete;
    private void Start()
    {
        if (instance == null)
            instance = this;
        levelComplete = PlayerPrefs.GetInt(LEVELINDEX);
    }
    public void isEndGame()
    {
        if (LevelManager.currentLevel == LEVELCOUNT)
        {
            //bool foundTriangle = false;
            //foreach (Transform child in levelManager.levelObjects[LEVELCOUNT].transform)
            //{
            //    if (child.CompareTag(TRIANGLE) && child.gameObject.activeSelf)
            //        foundTriangle = true;
            //}
            //if (!foundTriangle)
                LoadMenu();
        }
        else
        {
            if (levelComplete < LevelManager.currentLevel)
                PlayerPrefs.SetInt(LEVELINDEX, LevelManager.currentLevel);
        }
    }
    private void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
