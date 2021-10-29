using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If life bonus is collected, sound is played, additional life is being added to lives amount, and physical object is destroyed
public class LifeBonus : BonusCollect
{
    [SerializeField] private AudioClip healthSFX;
    public override void AddBonus()
    {
        FindObjectOfType<LivesManager>().AddLife();
        AudioSource.PlayClipAtPoint(healthSFX, Camera.main.transform.position, 0.1f);
        Destroy(gameObject);
    }
}
