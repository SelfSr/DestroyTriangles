using System.Collections;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    private const float DELAY_COROUTINE = 0.5f;
    private const string PLAYER = "Player";

    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Rigidbody2D newPlayer;
    [SerializeField] private GameObject newArrow;

    private int currentLevel;
    private Camera mainCamera;
    private GameObject player;
    private GameObject arrow;
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
                    ResetBall();
                    RespawnTriangle();
                }
            }
            yield return new WaitForSeconds(DELAY_COROUTINE);
        }
    }
    private void RespawnTriangle()
    {
        foreach (Transform child in levelManager.levelObjects[currentLevel].transform)
            child.gameObject.SetActive(true);
    }
    public void ResetBall()
    {
        arrow = GameObject.FindWithTag("Arrow");
        currentLevel = levelManager.currentLevel;
        gameManager.isOneBall = true;
        gameManager.isLockShootBall = false;
        Destroy(player);
        Destroy(arrow);
        gameManager.point = newPlayer;
        gameManager.arrowPrefab = newArrow;
    }
}
