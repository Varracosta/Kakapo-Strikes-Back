using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropingBombs : MonoBehaviour
{
    /*
     1) Set up cutscene timer to start dropping bombs
     2) set up 2nd timer to work while Kirov drops bombs (ends slightly before Kirov leaves the Scene)
     3) Instantiate bomb prefabs
     */
    [SerializeField] private GameObject bombPrefab;
    private float startToBombTimer = 79.8f;
    private float cutsceneLengthTimer = 26f;
    private const float BOMB_INTERVAL = 0.6f;
    private float bombTimer;

    void Start()
    {
        bombTimer = BOMB_INTERVAL;
    }

    private void Update()
    {
        if (FindObjectOfType<StartCutscene>().IsCutsceneStarted)
            StartBombing();
    }
    private void StartBombing()
    {
        startToBombTimer -= 1f * Time.deltaTime;

        if (startToBombTimer <= 0)
        {
            if (cutsceneLengthTimer <= 0)
                return;
            else
            {
                cutsceneLengthTimer -= 1f * Time.deltaTime;
                bombTimer -= 1f * Time.deltaTime;
                if (bombTimer <= 0)
                {
                    InstantiateBombs();
                    bombTimer = BOMB_INTERVAL;
                }
            }
        }
    }
    private void InstantiateBombs()
    {
        Instantiate(bombPrefab, transform.position, Quaternion.identity);
    }
}
