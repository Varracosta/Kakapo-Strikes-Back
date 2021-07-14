using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private Collider2D coll;
    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (coll.IsTouchingLayers(LayerMask.GetMask("Killable enemy")))
        {
            Destroy(other.gameObject);
        }
    }
}
