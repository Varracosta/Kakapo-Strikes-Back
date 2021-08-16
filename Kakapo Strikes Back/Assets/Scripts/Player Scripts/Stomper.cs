using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomper : MonoBehaviour
{
    [SerializeField] private BoxCollider2D stomper;
    private Kakapo kakapo;
    private int damage = 5;

    private Rigidbody2D rb;
    public float bounceForce = 20f;

    void Start()
    {
        kakapo = FindObjectOfType<Kakapo>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HurtBox"))
        {
            other.gameObject.GetComponent<EnemyHP>().TakeDamage(damage);
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        }

        if(kakapo.IsHurt == false)
        {
            if (other.gameObject.CompareTag("Spikes"))
            {
                kakapo.PerformKnockback(other);
                kakapo.TakeDamage(other.gameObject.GetComponent<DamageDealer>().GetDamage());
                Debug.Log("Feet touched");
            }
        }
        else { return; }
    }
}
