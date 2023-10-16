using UnityEngine;

public class Game : MonoBehaviour
{
    public Rigidbody2D projectilePrefab;
    public float maxPowerDistance = 5f;
    public float rotationSpeed = 50f;

    private Vector2 launchDirection;
    private Vector2 launchPowerPoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ������ ���� ������
        {
            SetLaunchDirection();
        }

        if (Input.GetMouseButton(0)) // ������ ���� ������������
        {
            UpdateLaunchPowerPoint();
        }

        if (Input.GetMouseButtonUp(0)) // ������ ���� ��������
        {
            LaunchProjectile();
        }
    }

    void SetLaunchDirection()
    {
        // ���������� ����������� ������ � ������ ������� ������ ����
        launchDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
    }

    void UpdateLaunchPowerPoint()
    {
        // ������� ������ ����� ������ ������ �� ����� ����������� ������ ����
        float rotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        launchPowerPoint = (Vector2)(Quaternion.Euler(0, 0, rotation) * (launchPowerPoint - (Vector2)transform.position)) + (Vector2)transform.position;
    }

    void LaunchProjectile()
    {
        float powerDistance = Vector2.Distance(launchPowerPoint, transform.position);
        float normalizedPower = Mathf.Clamp01(powerDistance / maxPowerDistance);
        float launchPower = normalizedPower * maxPowerDistance;

        Rigidbody2D projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectileInstance.velocity = launchDirection * launchPower;
    }
}
