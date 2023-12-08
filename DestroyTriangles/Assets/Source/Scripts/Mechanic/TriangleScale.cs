using Plugins.Audio.Core;
using UnityEngine;
using DG.Tweening;

public class TriangleScale : MonoBehaviour
{
    private PolygonCollider2D triangleCollaider;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private SourceAudio source;
    private bool isActiveState = false;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        triangleCollaider = GetComponent<PolygonCollider2D>();
    }
    private void OnEnable()
    {
        if (!isActiveState)
        {
            transform.DOShakeScale(0.5f, 0.1f, 7, 1, true, ShakeRandomnessMode.Harmonic);
            spriteRenderer.DOFade(0, 0);
            spriteRenderer.DOFade(1, 0.5f);
            isActiveState = true;
        }
    }

    private void OnDisable()
    {
        isActiveState = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            source.Play("CollishionTriangle");
            transform.DOShakeScale(0.2f, 0.07f, 7, 1, true, ShakeRandomnessMode.Harmonic);
            triangleCollaider.enabled = false;
            Invoke("ChangeVisiable", 0.2f);
        }
    }
    private void ChangeVisiable()
    {
        triangleCollaider.enabled = true;
        gameObject.SetActive(false);
    }
}
