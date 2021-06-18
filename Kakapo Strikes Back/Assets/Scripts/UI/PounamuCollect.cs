using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PounamuCollect : BonusCollect
{
    public override void AddBonus()
    {
        FindObjectOfType<UIManager>().AddToScore(50);
        Destroy(gameObject);
    }
}
