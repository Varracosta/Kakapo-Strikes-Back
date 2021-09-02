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
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "End game")
            GameScoreStats.instance.ResetScore();
    }
    public void StartLevel()
    {
        StartCoroutine(WaitAndStartFirstLevel());
    }
    private IEnumerator WaitAndStartFirstLevel()
    {
        yield return new WaitForSeconds(2.2f);
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

        if(sceneToContinue != 0) // make possible to load only playable levels, not total score after level scenes (make levels by even numbers perhaps)
        {
            if(PauseMenu.isPaused)
                FindObjectOfType<PauseMenu>().ResetPause();

            GameScoreStats.instance.ResetScore();
            GameScoreStats.instance.ResetLevelStats();
            SceneManager.LoadScene(sceneToContinue);
            Time.timeScale = 1f;
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
        StartCoroutine(WaitAndLoadNextLevel());
    }
    private IEnumerator WaitAndLoadNextLevel()
    {
        MusicPlayer.instance.PlayOnClick();
        yield return new WaitForSeconds(2.2f);
        SceneManager.LoadScene(currentSceneIndex + 1);
        GameScoreStats.instance.ResetLevelStats();
    }
    public void LoadTotalScoreScene()
    {
        StartCoroutine(WaitAndTotalScoreScene());
    }
    private IEnumerator WaitAndTotalScoreScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void LoadControllersScreen()
    {
        SceneManager.LoadScene(1);
    }
    public void GameOver()
    {
        StartCoroutine(WaitAndLoadGameOver());
    }
    private IEnumerator WaitAndLoadGameOver()
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
