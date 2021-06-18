using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusCollect : MonoBehaviour
{
    public abstract void AddBonus();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Kakapo"))
            AddBonus();
    }
}
