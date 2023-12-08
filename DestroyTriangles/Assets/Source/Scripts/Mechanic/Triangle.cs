using Plugins.Audio.Core;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    private Animator animator;
    private PolygonCollider2D triangleCollaider;
    [SerializeField] private SourceAudio source;
    private void Start()
    {
        animator = GetComponent<Animator>();
        triangleCollaider = GetComponent<PolygonCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Destroy", true);
            source.Play("CollishionTriangle");
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
