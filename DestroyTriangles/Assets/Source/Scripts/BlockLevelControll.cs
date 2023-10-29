using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlockLevelControll : MonoBehaviour
{
    private const string LEVELINDEX = "LevelIndex";
    private const int GAMESCENE = 1;
    [SerializeField] private List<Button> levelCounts;

    private int levelIndex;
    private void Start()
    {
        levelIndex = PlayerPrefs.GetInt(LEVELINDEX);
        foreach (var level in levelCounts)
            level.interactable = false;
        UnlockLevels(levelIndex);
    }
    public void LoadTo(int levelID)
    {
        SceneManager.LoadScene(GAMESCENE);
        PlayerPrefs.SetInt("SelectedLevel", levelID);
    }
    public void Reset()
    {
        foreach (var level in levelCounts)
            level.interactable = false;
        PlayerPrefs.DeleteAll();
    }
    private void UnlockLevels(int levelId)
    {
        if (levelId >= 1 && levelId <= levelCounts.Count)
        {
            for (int i = 0; i < levelId; i++)
                levelCounts[i].interactable = true;
        }
        else
        {
            Debug.LogError("Invalid levelId: " + levelId);
        }
    }
}
