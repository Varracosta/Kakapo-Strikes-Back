using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A separate script script for Player to stomp his enemies. Is placed on Player's child gameObject with it's own collider. 
public class Stomper : MonoBehaviour
{
    [SerializeField] private BoxCollider2D stomper;
    private Kakapo kakapo;
    private int damage = 5;

    private Rigidbody2D rigidBody;
    public float bounceForce = 20f;

    void Start()
    {
        kakapo = FindObjectOfType<Kakapo>();
        rigidBody = transform.parent.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (kakapo.IsHurt)
        {
            stomper.enabled = false;
        }
        else
        {
            stomper.enabled = true;
        }
    }

    //If Player falls on enemy with HurtBox component (designed specially for enemy to be stomped), then enemy will die, and Kakapo will bounce
    //Either if Player falls on Hedgehog ("Spikes"), Player will recieve damage. Because hedgehog has spikes, right? It should be killed differently
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HurtBox"))
        {
            other.gameObject.GetComponent<EnemyHP>().TakeDamage(damage);
            KillQuotes.instance.PlayKillPhrase();
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, bounceForce);
        }

        if(kakapo.IsHurt == false)
        {
            if (other.gameObject.CompareTag("Spikes"))
            {
                kakapo.PerformKnockback(other);
                kakapo.TakeDamage(other.gameObject.GetComponent<DamageDealer>().GetDamage());
            }
        }
        else { return; }
    }
}
