using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreStats : MonoBehaviour
{
    [SerializeField] private AudioClip bonusLifeSFX;
    [SerializeField] private GameObject bonusText;
    private int score = 0;
    private int bonus;
    private int bonusInterval = 1500;
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
            Instantiate(bonusText, FindObjectOfType<Kakapo>().transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(bonusLifeSFX, Camera.main.transform.position);
            LivesManager.instance.AddLife();
            bonus += bonusInterval;
        }
    }
    public void ResetScore()
    {
        score = 0;
    }
}
