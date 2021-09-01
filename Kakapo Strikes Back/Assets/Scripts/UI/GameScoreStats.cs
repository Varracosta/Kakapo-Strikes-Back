using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreStats : MonoBehaviour
{
    #region References
    [SerializeField] private AudioClip bonusLifeSFX;
    [SerializeField] private GameObject bonusText;
    #endregion

    #region Data
    private int score;
    private int killCount;
    private int bonus;
    private int bonusInterval = 1500;
    #endregion

    #region Lists for found items
    private List<GameObject> conesList;
    private List<GameObject> creaturesList;
    #endregion
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
        score = 0;
        killCount = 0;

        conesList = new List<GameObject>();
        creaturesList = new List<GameObject>();
    }
    private void OnEnable()
    {
        EnemyHP.EnemyKill += AddToKillCount;
    }
    private void Update()
    {
        AddBonusLifeForScore();
    }
    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }
    private void AddToKillCount()
    {
        killCount++;
    }
    public void AddToCreaturesList(Collider2D[] creatures)
    {
        foreach (Collider2D creature in creatures)
        {
            if (creaturesList.Exists(obj => obj == creature.gameObject))
                return;

            creaturesList.Add(creature.gameObject);
        }
    }
    public void AddToConesList(Collider2D[] cones)
    {
        foreach (Collider2D cone in cones)
        {
            if (conesList.Exists(obj => obj == cone.gameObject))
                return;

            conesList.Add(cone.gameObject);

        }
    }
    public void ResetLevelStats()
    {
        killCount = 0;
        creaturesList.Clear();
        conesList.Clear();
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
        killCount = 0;
    }
    public int GetScore() { return score; }
    public int GetKillCount() { return killCount; }
    public int GetConesCount() { return conesList.Count; }
    public int GetCreaturesCount() { return creaturesList.Count; }
}
