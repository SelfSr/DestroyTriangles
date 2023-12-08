using UnityEngine;

public class LevelList : MonoBehaviour
{
    [SerializeField] private GameObject[] levelPages;
    private int currentPageIndex = 0;

    public void SwitchNextPageLevel()
    {
        levelPages[currentPageIndex].SetActive(false);
        currentPageIndex++;
        if (currentPageIndex >= levelPages.Length)
        {
            currentPageIndex = 0;
        }
        levelPages[currentPageIndex].SetActive(true);
    }
    public void SwitchNextPreviousLevel()
    {
        levelPages[currentPageIndex].SetActive(false);
        currentPageIndex--;
        if (currentPageIndex < 0)
        {
            currentPageIndex = levelPages.Length - 1;
        }
        levelPages[currentPageIndex].SetActive(true);
    }
}
