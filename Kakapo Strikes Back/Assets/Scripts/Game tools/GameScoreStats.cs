using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Manages and stores info about Player's score, killcount, found creatures, found cones. 
//Also returns and resets it's values if needed (level restarts, game quits)
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
    private int bonus;
    private int bonusInterval = 1500;
    #endregion

    #region Lists for found items
    [SerializeField] private CreaturesStoringObject creaturesStoring;
    private List<GameObject> conesList = new List<GameObject>();
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
    //Determines what is Player according to current Level 
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
            PlayerDetermine();
            var _creature = creature.gameObject.GetComponent<Creature>().creature;
            creaturesStoring.AddCreature(_creature, creature.gameObject, player);
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
            cone.gameObject.GetComponent<FlashWhenFound>().PlayEffects(player);
        }
    }
    public void ResetLevelStats()
    {
        killCount = 0;
        creaturesStoring.ResetCreatureCount();
        conesList.Clear();
    }
    //if score reaches a certain value, an additional/bonus life will be added to Player
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
        creaturesStoring.ResetCreatureCount();
    }
    public int GetScore() { return score; }
    public int GetKillCount() { return killCount; }
    public int GetConesCount() { return conesList.Count; }
    public CreaturesStoringObject GetCreatureList() { return creaturesStoring; }
    public void SwitchOff()
    {
        isWorking = false;
    }
    private void OnApplicationQuit()
    {
        creaturesStoring.ClearContainer();   
    }
}
