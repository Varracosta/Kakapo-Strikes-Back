using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBonus : BonusCollect
{
    public override void AddBonus()
    {
        FindObjectOfType<LivesManager>().AddLife();
        Destroy(gameObject);
    }
}
