using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField] internal Kakapo kakapo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") ||
         other.gameObject.CompareTag("Spikes"))
        {
            kakapo.TakeDamage(other.gameObject.GetComponent<DamageDealer>().GetDamage());
        }
    }
}
