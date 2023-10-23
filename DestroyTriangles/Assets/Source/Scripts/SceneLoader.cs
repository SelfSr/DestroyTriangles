using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex;
    [SerializeField] private GameObject[] UIComponentsShow;
    [SerializeField] private GameObject[] UIComponentsHide;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
    public void ShowLevelList()
    {
        foreach (GameObject go in UIComponentsHide)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in UIComponentsShow)
        {
            go.SetActive(true);
        }
    }
}
