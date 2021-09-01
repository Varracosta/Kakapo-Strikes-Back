using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuotes : MonoBehaviour
{
    [SerializeField] private AudioClip[] killPhrases;
    private int counter = 0;
    public static KillQuotes instance;

    void Start()
    {
        instance = this;
    }

    public void PlayKillPhrase()
    {
        if (counter == killPhrases.Length)
            counter = 0;

        AudioSource.PlayClipAtPoint(killPhrases[counter], FindObjectOfType<Kakapo>().transform.position);
        counter++;
    }
}
