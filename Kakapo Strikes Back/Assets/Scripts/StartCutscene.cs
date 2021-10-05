using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;
    public bool IsCutsceneStarted { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Kakapo"))
        {
            IsCutsceneStarted = true;
            GameScoreStats.instance.SwitchOff();
            cutscene.SetActive(true);
            //MusicPlayer.instance.StopPlayingMusic();
        }
    }

}
