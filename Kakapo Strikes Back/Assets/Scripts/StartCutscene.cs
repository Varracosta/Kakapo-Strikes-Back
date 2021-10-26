using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;
    //[SerializeField] private GameObject lives;
    //[SerializeField] private GameObject scorePoints;

    [SerializeField] private GameObject[] UIElementsToHide;

    public bool IsCutsceneStarted { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Kakapo"))
        {
            StartCutScene();
            PlayerPrefs.SetString("LevelPassed", SceneManager.GetActiveScene().name);
        }
    }
    public void StartCutScene()
    {
        SwitchOnBool();
        GameScoreStats.instance.SwitchOff();

        foreach (GameObject element in UIElementsToHide)
        {
            element.SetActive(false);
        }

        //lives.SetActive(false);
        //scorePoints.SetActive(false);
        cutscene.SetActive(true);
    }

    public void SwitchOnBool() { IsCutsceneStarted = true; }
    public void SwitchOffBool() { IsCutsceneStarted = false; }
} 
