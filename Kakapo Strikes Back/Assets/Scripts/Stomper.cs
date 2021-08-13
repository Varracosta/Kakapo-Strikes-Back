using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomper : MonoBehaviour
{
    [SerializeField] private BoxCollider2D stomper;
    private int damage = 5;

    private Rigidbody2D rb;
    public float bounceForce = 20f;

    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HurtBox"))
        {
            other.gameObject.GetComponent<EnemyHP>().TakeDamage(damage);
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        }
    }
}
