using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField] internal Kakapo kakapo;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(kakapo.isHurt == false)
        {
           if (other.gameObject.CompareTag("Enemy") ||
                other.gameObject.CompareTag("Spikes"))
           {
                kakapo.TakeDamage(other.gameObject.GetComponent<DamageDealer>().GetDamage());
           }
        }
        else { return; }
    }

    //Killing enemies by stomping them
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (kakapo.stompBox.IsTouchingLayers(LayerMask.GetMask("Killable enemy")) &&
        !other.gameObject.CompareTag("Spikes"))
        {
            other.gameObject.GetComponent<DamageDealer>().TakeDamage(kakapo.damage);
            kakapo.rigidBody.velocity = new Vector2(kakapo.rigidBody.velocity.x, 15f);
        }
    }
}
