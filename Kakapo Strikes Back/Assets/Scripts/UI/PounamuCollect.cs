using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If pounamu stone is collected, sound is played, a certain amount is being added to score, and physical object is destroyed
public class PounamuCollect : BonusCollect
{
    [SerializeField] private AudioClip bonusSFX;

    public override void AddBonus()
    {
        FindObjectOfType<GameScoreStats>().AddToScore(50);
        AudioSource.PlayClipAtPoint(bonusSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
