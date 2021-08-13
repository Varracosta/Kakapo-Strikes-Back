using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private AudioClip gameOverSFX;
    private int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void StartLevel()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1f;
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }
    private IEnumerator LoadNextLevelCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void GameOver()
    {
        AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position, 0.1f);
        StartCoroutine(GameOverCoroutine());
    }
    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(1f);
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void QuitTheGame()
    {
        Application.Quit();
    }
}
