using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreDisplay;
    void Start()
    {
        scoreDisplay = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        scoreDisplay.text = UIManager.instance.GetScore().ToString();
    }
}
