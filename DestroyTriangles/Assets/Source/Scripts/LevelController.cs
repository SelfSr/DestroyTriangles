using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private const string LEVELINDEX = "LevelIndex";
    private const string TRIANGLE = "Triangle";
    public static int LEVELCOUNT = 96;

    public static LevelController instance = null;
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
