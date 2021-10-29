using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Plays music, and phrases before starting a level. Also switches off on Level 3 to not overlapping with 
// cutscene music  (I couldn't find how to do it more acurate)
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
