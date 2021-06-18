using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}
