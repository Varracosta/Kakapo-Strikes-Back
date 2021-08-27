using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private AudioClip collisionSFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HurtBox") || other.gameObject.CompareTag("Spikes"))
        {
            other.gameObject.GetComponent<EnemyHP>().Die();
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            AudioSource.PlayClipAtPoint(collisionSFX, Camera.main.transform.position);
        }
    }
}
