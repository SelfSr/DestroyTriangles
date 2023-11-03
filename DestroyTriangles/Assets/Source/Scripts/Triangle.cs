using Plugins.Audio.Core;
using UnityEngine;

public class Triangle : MonoBehaviour
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
            animator.SetBool("Destroy", true);
            source.Play("CollishionTriangle");
            Invoke("ChangeVisiable", 0.2f);
        }
    }
    private void ChangeVisiable()
    {
        gameObject.SetActive(false);
    }
}
