using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreaturesCountDisplay : MonoBehaviour
{
    private TextMeshProUGUI creaturesCountDisplay;
    void Start()
    {
        creaturesCountDisplay = GetComponent<TextMeshProUGUI>();   
    }

    // Update is called once per frame
    void Update()
    {
        creaturesCountDisplay.text = GameScoreStats.instance.GetCreaturesCount().ToString();
    }
}
