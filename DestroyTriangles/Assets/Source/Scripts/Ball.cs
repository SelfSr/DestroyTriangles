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
                ChangeColor(Color.cyan);
                break;
            case 3:
                ChangeColor(Color.blue);
                break;
            case 4:
                ChangeColor(Color.green);
                break;
            case 5:
                ChangeColor(Color.yellow);
                break;
            case 6:
                ChangeColor(Color.red);
                break;
            case 7:
                ChangeColor(Color.magenta);
                break;
            case 8:
                ChangeColor(Color.black);
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
