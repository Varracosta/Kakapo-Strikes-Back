using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : MonoBehaviour
{
    /*
     Wait for the end of timer 
     Call for DamageDealer.Die     
     */

    [SerializeField] private EnemyHP enemyHP;
    [SerializeField] private float timer = 50f;

    private void Update()
    {
        if (FindObjectOfType<StartCutscene>().IsCutsceneStarted)
            StartExplodeTimer();
    }

    private void StartExplodeTimer()
    {
        timer -= 1f * Time.deltaTime;

        if (timer <= 0)
        {
            enemyHP.Dying();
        }
    }
}
