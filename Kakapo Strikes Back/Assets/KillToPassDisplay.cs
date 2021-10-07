using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillToPassDisplay : MonoBehaviour
{
    [SerializeField] private int killToPassValue = 100;
    private TextMeshProUGUI killToPassText;
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
        killToPassText.text = killToPassValue.ToString();
        FinishLevel();
    }

    private void SubtractFromKillValue()
    {
        killToPassValue -= 1;
    }

    private void AddToKillValue()
    {
        killToPassValue += 1;
    }

    private void FinishLevel()
    {
        if(killToPassValue <= 0)
        {
            FindObjectOfType<StartCutscene>().StartCutScene();
        }
    }
}
