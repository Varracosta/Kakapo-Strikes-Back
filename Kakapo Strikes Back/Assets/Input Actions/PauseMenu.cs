using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private InputActions inputActions;
    public static bool isPaused = false; 

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }
    private void Start()
    {
        inputActions.UI.Pause.performed += _ => DeterminePause();
    }
    public void DeterminePause()
    {
        if (!isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
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
}
