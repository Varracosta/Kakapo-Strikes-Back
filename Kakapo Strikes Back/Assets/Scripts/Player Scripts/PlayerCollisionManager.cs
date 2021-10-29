using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If Player collides with an enemy (hedgehog included), a knockback will be performed and damage will be taken
public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField] internal Kakapo kakapo;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (kakapo.IsHurt == false)
        {
            if (other.gameObject.CompareTag("Enemy") ||
                other.gameObject.CompareTag("Hedgehog"))
            {
                kakapo.PerformKnockback(other);
                kakapo.TakeDamage(other.gameObject.GetComponent<DamageDealer>().GetDamage());
            }
        }
        else { return; }
    }
}
