using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public static bool isPaused;
    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
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
    public void DeterminePause()
    {
        if (!isPaused)
            PauseGame();
        else
            ResumeGme();
    }
    public void ResetPause()
    {
        if (isPaused)
            isPaused = false;

        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);
    }
    public void ResumeGme()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
    }
}
