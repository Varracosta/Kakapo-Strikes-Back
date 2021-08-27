using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int currentSceneIndex;
    private int sceneToContinue;
    
    public static SceneLoader instance;
    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "End game")
            GameScoreStats.instance.ResetScore();
    }
    public void StartLevel()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void SaveScene()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
    }
    public void Restart()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");

        if(sceneToContinue != 0)
        {
            GameScoreStats.instance.ResetScore();
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneToContinue);
        }
        else { return; }
    }
    public void BackToMenu()
    {
        if (PauseMenu.isPaused)
            FindObjectOfType<PauseMenu>().ResetPause();

        SceneManager.LoadScene(0);
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }
    public void LoadControllersScreen()
    {
        SceneManager.LoadScene(1);
    }
    private IEnumerator LoadNextLevelCoroutine()
    {
        yield return new WaitForSeconds(1f);
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
        GameScoreStats.instance.ResetScore();
    }
    public void QuitTheGame()
    {
        Application.Quit();
    }
}
