using Plugins.Audio.Core;
using UnityEngine;

public class TriangleDark : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private SourceAudio source;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.Play("DarkTriangle");
            source.Play("CollishionTriangle");
        }
    }
}
