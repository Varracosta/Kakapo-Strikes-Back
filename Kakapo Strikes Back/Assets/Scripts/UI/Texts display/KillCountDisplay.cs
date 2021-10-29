using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Displays killed amount of pests on Total Score panel
public class KillCountDisplay : MonoBehaviour
{
    private TextMeshProUGUI killCountDisplay;
    void Start()
    {
        killCountDisplay = GetComponent<TextMeshProUGUI>();   
    }

    void Update()
    {
        killCountDisplay.text = GameScoreStats.instance.GetKillCount().ToString();
    }
}
