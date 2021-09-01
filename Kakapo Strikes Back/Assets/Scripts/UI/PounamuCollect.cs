using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
