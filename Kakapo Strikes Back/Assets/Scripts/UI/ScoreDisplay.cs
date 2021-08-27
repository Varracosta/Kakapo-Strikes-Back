using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    //Displays score on the screen
    private TextMeshProUGUI scoreDisplay;
    void Start()
    {
        scoreDisplay = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        scoreDisplay.text = GameScoreStats.instance.GetScore().ToString();
    }
}
