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
        if (Input.GetMouseButtonDown(0)) // кнопка мыши нажата
        {
            SetLaunchDirection();
        }

        if (Input.GetMouseButton(0)) // кнопка мыши удерживается
        {
            UpdateLaunchPowerPoint();
        }

        if (Input.GetMouseButtonUp(0)) // кнопка мыши отпущена
        {
            LaunchProjectile();
        }
    }

    void SetLaunchDirection()
    {
        // Определяем направление полета в момент нажатия кнопки мыши
        launchDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
    }

    void UpdateLaunchPowerPoint()
    {
        // Вращаем вторую точку вокруг первой во время удерживания кнопки мыши
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
