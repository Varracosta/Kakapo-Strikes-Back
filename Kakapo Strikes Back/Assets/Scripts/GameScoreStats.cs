using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreStats : MonoBehaviour
{
    private int score = 0;
    private int bonus;
    private int bonusInterval = 500;
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
        bonus = bonusInterval;
    }
    private void Update()
    {
        AddBonusLifeForScore();
    }

    public float GetScore() { return score; }
    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }
    private void AddBonusLifeForScore()
    {
        if(score >= bonus)
        {
            FindObjectOfType<Kakapo>().InstantiatePopUp();
            LivesManager.instance.AddLife();
            bonus += bonusInterval;
        }
    }
    public void ResetScore()
    {
        score = 0;
    }
}
