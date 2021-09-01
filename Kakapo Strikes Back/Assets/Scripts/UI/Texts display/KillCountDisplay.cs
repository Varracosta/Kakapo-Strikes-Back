using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCountDisplay : MonoBehaviour
{
    private TextMeshProUGUI killCountDisplay;
    void Start()
    {
        killCountDisplay = GetComponent<TextMeshProUGUI>();   
    }

    // Update is called once per frame
    void Update()
    {
        killCountDisplay.text = GameScoreStats.instance.GetKillCount().ToString();
    }
}
