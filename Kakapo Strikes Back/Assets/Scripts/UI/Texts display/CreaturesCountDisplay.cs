using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Displays found amount of creatures on Total Score panel
public class CreaturesCountDisplay : MonoBehaviour
{
    [SerializeField] private CreaturesStoringObject creatureStoring;
    private TextMeshProUGUI creaturesCountDisplay;
    void Start()
    {
        creaturesCountDisplay = GetComponent<TextMeshProUGUI>();   
    }
    void Update()
    {
        creaturesCountDisplay.text = creatureStoring.GetCreatureCount().ToString();
    }
}
