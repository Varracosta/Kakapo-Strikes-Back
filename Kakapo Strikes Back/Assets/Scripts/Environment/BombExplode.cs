using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Triggers animation and plays sound if a bomb hits something
public class BombExplode : MonoBehaviour
{
    [SerializeField] private AudioClip explosionSFX;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HurtBox") ||
                other.gameObject.CompareTag("Spikes"))
        {
            other.gameObject.GetComponent<EnemyHP>().Die();
            StartCoroutine(WaitAndExplode());
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(WaitAndExplode());
        }
    }
    private IEnumerator WaitAndExplode()
    {
        AudioSource.PlayClipAtPoint(explosionSFX, transform.position);
        animator.SetBool("Explode", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(transform.parent.gameObject);
    }
}
