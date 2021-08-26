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

    private float score = 0f;
    public static UIManager instance;

    private void Awake()
    {
        if (FindObjectsOfType<UIManager>().Length > 1)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public float GetScore() { return score; }
    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetScore()
    {
        score = 0f;
    }
}
