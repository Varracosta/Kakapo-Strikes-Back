using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Displays found amount of cones on Total Score panel
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
