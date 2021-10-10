using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScoreStats : MonoBehaviour
{
    #region References
    [SerializeField] private AudioClip bonusLifeSFX;
    [SerializeField] private GameObject bonusText;
    [SerializeField] private GameObject creatureText;
    [SerializeField] private GameObject coneText;
    private GameObject player;
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
    private bool isWorking;
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
        if (isWorking)
            AddBonusLifeForScore();
    }
    private void PlayerDetermine()
    {
        if (SceneManager.GetActiveScene().name == "Level 4")
            player = FindObjectOfType<PlayerFlying>().gameObject;
        else
            player = FindObjectOfType<Kakapo>().gameObject;
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

            PlayerDetermine();
            creaturesList.Add(creature.gameObject);
            DisplayCreature.instance.DisplayFoundCreature(creature.gameObject);
            creaturesCount++;
            creature.gameObject.GetComponent<FlashWhenFound>().PlayFlashAndSound();
            Instantiate(creatureText, player.transform.position, Quaternion.identity);
        }
    }
    public void AddToConesList(Collider2D[] cones)
    {
        foreach (Collider2D cone in cones)
        {
            if (conesList.Exists(obj => obj == cone.gameObject))
                return;

            PlayerDetermine();
            conesList.Add(cone.gameObject);
            cone.gameObject.GetComponent<FlashWhenFound>().PlayFlashAndSound();
            Instantiate(coneText, player.transform.position, Quaternion.identity);
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
            PlayerDetermine();
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
