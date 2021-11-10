using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Script responsible for managing and displaying kill count (Level 4). 
   Player will pass the level if they'll kill a certain amount of pests. Script recieves info from two events - whenever enemy is killed or skipped/missed. 
    If enemy is killed - the value will be subtracted from total amount; if enemy is not killed - the value will be added to total amount
*/
public class KillToPassDisplay : MonoBehaviour
{
    public int KillToPassValue { get; private set; } = 50;
    private TextMeshProUGUI killToPassText;

    public delegate void OnLowKillValue();
    public static event OnLowKillValue lowKillValue;

    void Start()
    {
        killToPassText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        FlyingEnemy.FlyingEnemyKill += SubtractFromKillValue;
        EnemyShredder.MissedEnemy += AddToKillValue;
    }
    void Update()
    {
        killToPassText.text = KillToPassValue.ToString();
        FinishLevel();
    }
    private void SubtractFromKillValue()    {  KillToPassValue -= 1;   }
    private void AddToKillValue()   {   KillToPassValue += 1;   }
    private void FinishLevel()
    {
        if(KillToPassValue <= 0)
        {
            lowKillValue?.Invoke();
            FindObjectOfType<StartCutscene>().StartCutScene();
        }
    }
}
