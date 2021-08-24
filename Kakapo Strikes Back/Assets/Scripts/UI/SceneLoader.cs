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
            UIManager.instance.ResetPause();
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneToContinue);
        }
        else { return; }
    }
    public void BackToMenu()
    {
        UIManager.instance.ResetPause();
        SceneManager.LoadScene(0);
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
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
    }
    public void QuitTheGame()
    {
        Application.Quit();
    }
}
