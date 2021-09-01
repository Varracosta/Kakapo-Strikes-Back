using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConesCountDisplay : MonoBehaviour
{
    private TextMeshProUGUI conesCountDisplay;
    void Start()
    {
        conesCountDisplay = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        conesCountDisplay.text = GameScoreStats.instance.GetConesCount().ToString();
    }
}
