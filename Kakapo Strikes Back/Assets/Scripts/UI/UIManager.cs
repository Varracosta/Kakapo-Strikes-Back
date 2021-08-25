using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Kakapo kakapo;
    [SerializeField] private TextMeshProUGUI scoreText;

    private float score = 0f;
    public static UIManager instance;

    private void Start()
    {
        instance = this;
    }

    private void Update()
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
