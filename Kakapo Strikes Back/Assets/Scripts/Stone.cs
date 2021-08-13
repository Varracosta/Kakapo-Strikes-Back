using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HurtBox") || other.gameObject.CompareTag("Spikes"))
        {
            other.gameObject.GetComponent<EnemyHP>().Die();
        }
    }
}
