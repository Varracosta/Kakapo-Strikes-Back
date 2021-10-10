using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
