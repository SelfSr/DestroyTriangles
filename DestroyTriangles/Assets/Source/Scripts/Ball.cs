using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private HardLight2D HardLight2D;
    [SerializeField] private TrailRenderer TrailRenderer;
    [SerializeField] private SpriteRenderer SpriteRenderer;

    private int collisionCount = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionCount++;
        switch (collisionCount)
        {
            case 2:
                ChangeColor(Color.green);
                break;
            case 3:
                ChangeColor(Color.red);
                break;
            case 4:
                ChangeColor(Color.magenta);
                break;
            default:
                break;
        }
    }
    private void ChangeColor(Color color)
    {
        SpriteRenderer.color = color;
        TrailRenderer.startColor = color;
        HardLight2D.Color = color;
    }
}
