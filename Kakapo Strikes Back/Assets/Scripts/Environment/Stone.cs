using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Responsible for starting dying scripts of Enemies, and play kill quotes/sounds if hits an enemy/ground 
public class Stone : MonoBehaviour
{
    [SerializeField] private AudioClip collisionSFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HurtBox") || other.gameObject.CompareTag("Spikes"))
        {
            other.gameObject.GetComponent<EnemyHP>().Die();
            KillQuotes.instance.PlayKillPhrase();
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            AudioSource.PlayClipAtPoint(collisionSFX, Camera.main.transform.position);
        }
    }
}
