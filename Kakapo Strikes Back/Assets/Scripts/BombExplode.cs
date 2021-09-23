using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    private Animator animator;
    private CapsuleCollider2D capCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        capCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        //Explode();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") ||
                other.gameObject.CompareTag("Hedgehog"))
        {
            other.gameObject.GetComponentInChildren<EnemyHP>().Dying();
        }

        //animator.SetBool("Explode", true);
        //StartCoroutine(WaitAndExplode());
    }

    private void Explode()
    {
        if (capCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("Explode", true);
            StartCoroutine(WaitAndExplode());
        }
    }

    private IEnumerator WaitAndExplode()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
