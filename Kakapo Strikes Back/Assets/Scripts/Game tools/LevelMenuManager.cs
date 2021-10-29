using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Manages which level can be unlocked according to Player game progress; loads desired level
//Also resets level info if you quit the game
public class LevelMenuManager : MonoBehaviour
{
    [SerializeField] private Button level02Button, level03Button, level04Button;

    void Start()
    {
        level02Button.interactable = false;
        level03Button.interactable = false;
        level04Button.interactable = false;

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
            case "Level 3":
                level02Button.interactable = true;
                level03Button.interactable = true;
                level04Button.interactable = true;
                break;
        }
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
