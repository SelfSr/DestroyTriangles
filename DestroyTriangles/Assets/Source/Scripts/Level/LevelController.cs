using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LevelController : MonoBehaviour
{
    private const string LEVELINDEX = "LevelIndex";
    public static int LEVELCOUNT = 73;

    [SerializeField] private LeaderboardYG leaderboardYG;

    private int maxCompletedLevel;

    public static LevelController instance = null;
    private void Start()
    {
        if (instance == null)
            instance = this;
        maxCompletedLevel = PlayerPrefs.GetInt(LEVELINDEX, 1);
    }
    public void isEndGame()
    {
        int currentLevel = LevelManager.currentLevel;
        if (currentLevel > maxCompletedLevel)
        {
            maxCompletedLevel = currentLevel;
            PlayerPrefs.SetInt(LEVELINDEX, maxCompletedLevel);
        }
        UpdateLeaderboard(maxCompletedLevel);
    }
    private void UpdateLeaderboard(int maxCompletedLevel)
    {
        YandexGame.NewLeaderboardScores(leaderboardYG.nameLB, maxCompletedLevel == LEVELCOUNT ? LEVELCOUNT - 1 : maxCompletedLevel);
    }
    private void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
