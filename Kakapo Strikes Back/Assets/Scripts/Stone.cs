using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    //[SerializeField] private BoxCollider2D stomper;

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    GameObject enemy = other.gameObject;

    //    if (stomper.IsTouchingLayers(LayerMask.GetMask("Killable enemy")))
    //    {
    //        enemy.gameObject.GetComponent<EnemyHP>().Die();
    //        enemy.gameObject.GetComponentInChildren<EnemyHP>().Die();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Spikes"))
        {
            Debug.Log("I touch enemy");
            //other.gameObject.GetComponentInChildren<EnemyHP>().Die();
            other.gameObject.GetComponent<EnemyHP>().Die();
        }
    }
}
