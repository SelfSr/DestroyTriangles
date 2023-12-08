using DG.Tweening;
using Plugins.Audio.Core;
using UnityEngine;

public class TriangleDarkScale : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private SourceAudio source;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.DOShakeScale(0.5f, 0.1f, 7, 1, true, ShakeRandomnessMode.Harmonic);
        spriteRenderer.DOFade(0, 0);
        spriteRenderer.DOFade(1, 0.5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            source.Play("CollishionTriangle");
            transform.DOShakeScale(0.2f, 0.07f, 7, 1, true, ShakeRandomnessMode.Harmonic);
        }
    }
}
