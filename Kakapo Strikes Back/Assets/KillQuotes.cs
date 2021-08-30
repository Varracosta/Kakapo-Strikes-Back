using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuotes : MonoBehaviour
{
    [SerializeField] private AudioClip[] killPhrases;
    private int randomFactor;
    public static KillQuotes instance;

    void Start()
    {
        instance = this;
        randomFactor = Random.Range(0, 4);
    }

    public void PlayKillPhrase()
    {
        AudioSource.PlayClipAtPoint(killPhrases[randomFactor], FindObjectOfType<Kakapo>().transform.position);
        randomFactor = Random.Range(0, 4);
    }
}
