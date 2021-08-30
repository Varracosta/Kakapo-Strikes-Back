using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private AudioClip startFirstLevelSFX;
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
        StartCoroutine(WaitAndStartFirstLevel());
    }
    private IEnumerator WaitAndStartFirstLevel()
    {
        AudioSource.PlayClipAtPoint(startFirstLevelSFX, Camera.main.transform.position, 0.5f);
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

        if(sceneToContinue != 0)
        {
            if(PauseMenu.isPaused)
                FindObjectOfType<PauseMenu>().ResetPause();

            GameScoreStats.instance.ResetScore();
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
    public void LoadControllersScreen()
    {
        SceneManager.LoadScene(1);
    }
    private IEnumerator WaitAndLoadNextLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(currentSceneIndex + 1);
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
