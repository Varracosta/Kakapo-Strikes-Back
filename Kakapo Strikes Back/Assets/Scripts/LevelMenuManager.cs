using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour
{
    [SerializeField] private Button level02Button, level03Button;

    void Start()
    {
        level02Button.interactable = false;
        level03Button.interactable = false;

        string levelName = PlayerPrefs.GetString("LevelPassed");
        switch (levelName)
        {
            case "Level 1":
                level02Button.interactable = true;
                break;
            case "Level 2":
                level02Button.interactable = true;
                level03Button.interactable = true;
                break;
        }
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
