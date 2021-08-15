using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField] internal Kakapo kakapo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if(kakapo.IsHurt == false)
        //{
        //    if (other.gameObject.CompareTag("Enemy") ||
        //        other.gameObject.CompareTag("Hedgehog"))
        //    {
        //        if (other.gameObject.transform.position.x > transform.position.x)
        //        {
        //            FindObjectOfType<Kakapo>().rigidBody.velocity = new Vector2(-5f, 12f);
        //            kakapo.TakeDamage(other.gameObject.GetComponent<DamageDealer>().GetDamage());
        //        }
        //        else if (other.gameObject.transform.position.x < transform.position.x)
        //        {
        //            FindObjectOfType<Kakapo>().rigidBody.velocity = new Vector2(5f, 12f);
        //            kakapo.TakeDamage(other.gameObject.GetComponent<DamageDealer>().GetDamage());
        //        }

        //        Debug.Log("Body touched");
        //    }
        //}
        //else { return; }

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
