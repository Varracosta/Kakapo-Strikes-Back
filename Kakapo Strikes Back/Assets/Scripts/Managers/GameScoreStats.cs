using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreStats : MonoBehaviour
{
    #region References
    [SerializeField] private AudioClip bonusLifeSFX;
    [SerializeField] private GameObject bonusText;
    [SerializeField] private GameObject creatureText;
    [SerializeField] private GameObject coneText;
    [SerializeField] private GameObject player;
    #endregion

    #region Data
    private int score;
    private int killCount;
    private int creaturesCount;
    private int bonus;
    private int bonusInterval = 1500;
    #endregion

    #region Lists for found items
    private List<GameObject> conesList = new List<GameObject>();
    private List<GameObject> creaturesList = new List<GameObject>();
    #endregion
    public bool isWorking;
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

        isWorking = true;
        bonus = bonusInterval;
        score = 0;
        killCount = 0;
        creaturesCount = 0;
    }
    private void OnEnable()
    {
        EnemyHP.EnemyKill += AddToKillCount;
    }
    private void Update()
    {
        if(isWorking)
            AddBonusLifeForScore();
    }
    public void AddToScore(int scoreValue)  {   score += scoreValue; }
    public void SubtractFromScore(int scoreValue) { score -= scoreValue; }
    private void AddToKillCount() { killCount++;    }
    public void AddToCreaturesList(Collider2D[] creatures)
    {
        foreach (Collider2D creature in creatures)
        {
            if (creaturesList.Exists(obj => obj == creature.gameObject))
                return;

            creaturesList.Add(creature.gameObject);
            DisplayCreature.instance.DisplayFoundCreature(creature.gameObject);
            creaturesCount++;
            creature.gameObject.GetComponent<FlashWhenFound>().PlayFlashAndSound();
            Instantiate(creatureText, FindObjectOfType<Kakapo>().transform.position, Quaternion.identity);
        }
    }
    public void AddToConesList(Collider2D[] cones)
    {
        foreach (Collider2D cone in cones)
        {
            if (conesList.Exists(obj => obj == cone.gameObject))
                return;

            conesList.Add(cone.gameObject);
            cone.gameObject.GetComponent<FlashWhenFound>().PlayFlashAndSound();
            Instantiate(coneText, FindObjectOfType<Kakapo>().transform.position, Quaternion.identity);
        }
    }
    public void ResetLevelStats()
    {
        killCount = 0;
        creaturesCount = 0;
        conesList.Clear();
    }
    private void AddBonusLifeForScore()
    {
        if(score >= bonus)
        {
            Instantiate(bonusText, player.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(bonusLifeSFX, player.transform.position);
            LivesManager.instance.AddLife();
            bonus += bonusInterval;
        }
    }
    public void ResetScore()
    {
        score = 0;
        killCount = 0;
        creaturesCount = 0;
    }
    public int GetScore() { return score; }
    public int GetKillCount() { return killCount; }
    public int GetConesCount() { return conesList.Count; }
    public int GetCreaturesCount() { return creaturesCount; }
    public List<GameObject> GetCreatureList() { return creaturesList; }
    public void SwitchOff()
    {
        isWorking = false;
    }
}
