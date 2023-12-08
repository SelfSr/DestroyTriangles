using DG.Tweening;
using System.Collections;
using UnityEngine;
using YG;

public class RestartLevel : MonoBehaviour
{
    private const float DELAY_COROUTINE = 0.5f;
    private const string PLAYER = "Player";

    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Rigidbody2D newPlayer;
    [SerializeField] private GameObject newArrow;
    [SerializeField] public GameObject loseWindow;
    [SerializeField] private GameObject[] UIElements;

    private int currentLevel;
    private Camera mainCamera;
    private GameObject player;
    private GameObject arrow;
    private bool isOneCheckAD = true;
    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(CheckPlayerPosition());
    }
    private IEnumerator CheckPlayerPosition()
    {
        while (true)
        {
            player = GameObject.FindWithTag(PLAYER);
            if (player != null)
            {
                Vector3 viewportPosition = mainCamera.WorldToViewportPoint(player.transform.position);
                if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
                {
                    if (isOneCheckAD)
                    {
                        foreach (GameObject ui in UIElements)
                            ui.SetActive(false);

                        AnimateSequenceLoseWindow();

                        ResetBall();
                        Invoke("ShowFullScreenAd", 1f);
                        isOneCheckAD = false;
                    }
                }
            }
            yield return new WaitForSeconds(DELAY_COROUTINE);
        }
    }
    public void RespawnTriangle()
    {
        foreach (Transform child in levelManager.levelObjects[currentLevel].transform)
            child.gameObject.SetActive(true);
    }
    public void ResetBall()
    {
        arrow = GameObject.FindWithTag("Arrow");
        currentLevel = LevelManager.currentLevel;
        gameManager.isOneBall = true;
        gameManager.isLockShootBall = false;
        Destroy(player);
        Destroy(arrow);
        gameManager.point = newPlayer;
        gameManager.arrowPrefab = newArrow;
    }
    public void Restart()
    {
        ResetBall();
        Invoke("RespawnTriangle", 0.3f);
        if (loseWindow.activeSelf == true)
        {
            foreach (GameObject ui in UIElements)
                ui.SetActive(true);
            loseWindow.SetActive(false);
        }
        else
        {
            YandexGame.FullscreenShow();
        }
    }
    private void AnimateSequenceLoseWindow()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(loseWindow.transform.DOScale(1, 0))
            .Join(loseWindow.GetComponent<CanvasGroup>().DOFade(0, 0))
            .Append(loseWindow.GetComponent<CanvasGroup>().DOFade(1, 0.2f))
            .OnStart(() => loseWindow.SetActive(true));
    }
    private void ShowFullScreenAd()
    {
        YandexGame.FullscreenShow();
        isOneCheckAD = true;
    }
}
