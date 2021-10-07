using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;
    [SerializeField] private GameObject lives;
    [SerializeField] private GameObject scorePoints;
    public bool IsCutsceneStarted { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Kakapo"))
        {
            StartCutScene();
        }
    }

    public void StartCutScene()
    {
        IsCutsceneStarted = true;
        GameScoreStats.instance.SwitchOff();
        lives.SetActive(false);
        scorePoints.SetActive(false);
        cutscene.SetActive(true);
    }
}
