using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Kakapo kakapo;
    [SerializeField] private TextMeshProUGUI scoreText;

    private float score = 0f;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetScore()
    {
        score = 0f;
    }
}
