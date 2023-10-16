using TMPro;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    [SerializeField] private Rigidbody2D point;
    [SerializeField] private TextMeshProUGUI powerPresentText;

    private RectTransform textRectTransform;
    private Camera mainCamera;

    private Vector2 worldStartMousePosition;
    private Vector2 worldEndMousePosition;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            worldStartMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            print(worldStartMousePosition);
            point = Instantiate(point, worldStartMousePosition, Quaternion.identity);
        }
        if (Input.GetMouseButton(0))
        {
            worldEndMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            RectTransformUtility.ScreenPointToWorldPointInRectangle(powerPresentText.rectTransform, mainCamera.WorldToScreenPoint(point.position), mainCamera, out Vector3 worldPointPos);

            // Устанавливаем позицию текста в мировых координатах point
            powerPresentText.rectTransform.position = worldPointPos;

            //Vector2 screenPoint = mainCamera.WorldToScreenPoint(point.position);
            //powerPresentText.rectTransform.position = screenPoint;
            //powerPresentText.enabled = true;

            point.position = new Vector2(worldEndMousePosition.x, worldEndMousePosition.y);
        }
    }
}
