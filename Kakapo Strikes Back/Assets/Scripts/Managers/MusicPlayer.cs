using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] startLevelPhrasesSFX;
    private int phraseCounter;
    public static MusicPlayer instance;
    public void Awake()
    {
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        phraseCounter = 0;
    }
    private void Update()
    {
        MusicSetUp();
    }
    public void PlayOnClick()
    {
        AudioSource.PlayClipAtPoint(startLevelPhrasesSFX[phraseCounter], Camera.main.transform.position);
        phraseCounter++;
    }

    private void MusicSetUp() 
    {
        if (SceneManager.GetActiveScene().name == "Level 3")
        {
                AudioListener.pause = FindObjectOfType<StartCutscene>().IsCutsceneStarted;
        }
    }
}
