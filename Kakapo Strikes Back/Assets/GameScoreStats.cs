using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreStats : MonoBehaviour
{
    [SerializeField] private Kakapo kakapo;

    private float score = 0f;
    public static GameScoreStats instance;

    private void Awake()
    {
        if (FindObjectsOfType<GameScoreStats>().Length > 1)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public float GetScore() { return score; }
    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetScore()
    {
        score = 0f;
    }
}
