using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
