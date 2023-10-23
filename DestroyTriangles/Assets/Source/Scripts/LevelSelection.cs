using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    private int nextSceneIndex = 1;
    [SerializeField] private TextMeshProUGUI indexLevel;
    public void SelectLevel()
    {
        SceneManager.LoadScene(nextSceneIndex);
        PlayerPrefs.SetInt("SelectedLevel", int.Parse(indexLevel.text));
    }
}
