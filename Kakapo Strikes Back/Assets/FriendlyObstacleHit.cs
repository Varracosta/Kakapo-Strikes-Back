using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyObstacleHit : MonoBehaviour
{
    [SerializeField] private int scoreSubtract = 200;
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
        GameScoreStats.instance.SubtractFromScore(scoreSubtract);
        Destroy(gameObject);
    }
}
