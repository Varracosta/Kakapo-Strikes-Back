using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Destroys any bullet that didn't hit any target
public class BulletShredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
