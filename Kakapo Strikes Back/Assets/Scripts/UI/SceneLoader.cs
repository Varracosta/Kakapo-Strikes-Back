using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    private int currentSceneIndex;

    public static SceneLoader sceneLoader;
    private void Start()
    {
        sceneLoader = this;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void StartLevel()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Restart()
    {
        UIManager.instance.ResetPause();
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
        StartCoroutine(GameOverCoroutine());
    }
    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene("GameOver");
    }
    public void QuitTheGame()
    {
        Application.Quit();
    }
}
