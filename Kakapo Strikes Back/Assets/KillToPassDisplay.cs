using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillToPassDisplay : MonoBehaviour
{
    public int KillToPassValue { get; private set; } = 50;
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
        killToPassText.text = KillToPassValue.ToString();
        FinishLevel();
    }

    private void SubtractFromKillValue()
    {
        KillToPassValue -= 1;
    }

    private void AddToKillValue()
    {
        KillToPassValue += 1;
    }

    private void FinishLevel()
    {
        if(KillToPassValue <= 0)
        {
            FindObjectOfType<StartCutscene>().StartCutScene();
        }
    }
}
