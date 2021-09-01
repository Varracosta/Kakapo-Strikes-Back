using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void PlayOnClick()
    {
        AudioSource.PlayClipAtPoint(startLevelPhrasesSFX[phraseCounter], Camera.main.transform.position);
        phraseCounter++;
    }
}
