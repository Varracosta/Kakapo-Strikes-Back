using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;
    int lastSceneIndex;
    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        lastSceneIndex = currentSceneIndex;
    }
    public void StartLevel()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Restart()
    {
        SceneManager.LoadScene(lastSceneIndex);
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }
    private IEnumerator LoadNextLevelCoroutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }
    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game Over");
    }
}
