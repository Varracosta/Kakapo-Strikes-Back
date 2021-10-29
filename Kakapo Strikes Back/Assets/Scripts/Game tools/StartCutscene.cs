using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Runs cutscene on trigger and switches off any UIElements that are added to an array 
public class StartCutscene : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;
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
        cutscene.SetActive(true);
    }

    public void SwitchOnBool() { IsCutsceneStarted = true; }
    public void SwitchOffBool() { IsCutsceneStarted = false; }
} 
