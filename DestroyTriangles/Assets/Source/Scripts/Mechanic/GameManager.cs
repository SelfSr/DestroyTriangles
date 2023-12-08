using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private const float TEXT_OFFSET_FACTOR = 1.5f;
    private const float PERCENTAGE = 100f;
    private const float MULTIPLY_FORCE = 20f;
    private const string TRIANGLE = "Triangle";
    private const string TRIANGLE_DARK = "TriangleDark";
    private const string UI = "UI";

    [SerializeField] public Rigidbody2D point;
    [SerializeField] private TextMeshProUGUI powerPrecentText;
    [SerializeField] public GameObject arrowPrefab;
    [SerializeField] private float maxVectorLength = 1.5f;
    [SerializeField] private RestartLevel restartLevelScript;

    private Camera mainCamera;
    private Vector2 worldStartMousePosition;
    private Vector2 worldEndMousePosition;
    private Vector2 forceDirection;

    private float _vectorLength;
    [HideInInspector] public bool isOneBall = true;
    [HideInInspector] public bool isLockShootBall = false;

    private GameObject tempArrowPrefab;

    private void Start()
    {
        mainCamera = Camera.main;
        tempArrowPrefab = arrowPrefab;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (restartLevelScript.loseWindow.activeSelf == false)
                SpawnBall();
        }
        if (Input.GetMouseButton(0))
        {
            AddForceBall();
            SpawnPowerPercentText();
        }
        if (Input.GetMouseButtonUp(0))
        {
            ShootingBall();
            powerPrecentText.enabled = false;
        }
    }
    private void SpawnBall()
    {
        if (isOneBall)
        {
            worldStartMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldStartMousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag != TRIANGLE && hit.collider.gameObject.tag != TRIANGLE_DARK && !IsPointerOverUI())
                {
                    arrowPrefab = tempArrowPrefab;
                    point = Instantiate(point, worldStartMousePosition, Quaternion.identity);
                    arrowPrefab = Instantiate(arrowPrefab, worldStartMousePosition, Quaternion.identity);
                    arrowPrefab.SetActive(true);
                    isLockShootBall = true;
                }
            }
        }
    }
    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    private void AddForceBall()
    {
        if (isOneBall)
        {
            worldEndMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _vectorLength = Mathf.Clamp(Vector2.Distance(worldStartMousePosition, worldEndMousePosition), 0f, maxVectorLength);
            point.position = (Vector2)point.position;
            Debug.DrawLine(worldStartMousePosition, worldEndMousePosition, Color.red);
        }
    }
    private void ShootingBall()
    {
        if (isOneBall)
        {
            forceDirection = (worldStartMousePosition - worldEndMousePosition).normalized;
            point.velocity = forceDirection * Mathf.Round(_vectorLength / maxVectorLength * PERCENTAGE) / MULTIPLY_FORCE;
            if (isLockShootBall)
            {
                Destroy(arrowPrefab);
                powerPrecentText.enabled = false;
                isOneBall = false;
            }
        }
    }
    private void SpawnPowerPercentText()
    {
        if (isLockShootBall)
        {
            powerPrecentText.enabled = true;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(powerPrecentText.rectTransform, mainCamera.WorldToScreenPoint(point.position), mainCamera, out Vector3 worldPointPos);
            powerPrecentText.rectTransform.position = new Vector3(worldPointPos.x + TEXT_OFFSET_FACTOR, worldPointPos.y, worldPointPos.z);
            powerPrecentText.text = ConvertToPercentage(_vectorLength, maxVectorLength) + "%";
        }
    }
    private string ConvertToPercentage(float vectorLength, float maxVectorLength)
    {
        return Mathf.Round(vectorLength / maxVectorLength * PERCENTAGE).ToString();
    }
}
