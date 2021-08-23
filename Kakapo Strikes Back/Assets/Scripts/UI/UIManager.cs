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
    [SerializeField] private GameObject pauseMenu;

    private float score = 0f;
    public static bool isPaused;

    private InputActions inputActions;
    public static UIManager instance;
    private void Awake()
    {
        inputActions = new InputActions();
    }
    private void Start()
    {
        instance = this;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.UI.Pause.performed += _ => DeterminePause();
    }
    private void OnDisable()
    {
        inputActions.UI.Pause.performed -= _ => DeterminePause();
        inputActions.Disable();
    }
    private void Update()
    {
        scoreText.text = score.ToString();
    }
    public void DeterminePause()
    {
        if (!isPaused)
            PauseGame();
        else
            ResumeGame();
    }
    public void ResetPause()
    {
        isPaused = !isPaused;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
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
