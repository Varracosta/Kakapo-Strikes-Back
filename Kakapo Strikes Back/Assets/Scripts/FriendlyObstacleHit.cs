using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A script for a friendly obstacle (kereru) on Level 4. If Player hits it, or shoots - an amount from score will be subtracted;
// it is accompanied with sound.    
public class FriendlyObstacleHit : MonoBehaviour
{
    [SerializeField] private int scoreSubtract = 200;
    [SerializeField] private AudioClip screechSFX;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            SubtractScoreAndDestroy();
        }

        if (other.gameObject.CompareTag("Player"))
        {
            SubtractScoreAndDestroy();
        }
    }

    private void SubtractScoreAndDestroy()
    {
        AudioSource.PlayClipAtPoint(screechSFX, transform.position);
        GameScoreStats.instance.SubtractFromScore(scoreSubtract);
        Destroy(gameObject);
    }
}
