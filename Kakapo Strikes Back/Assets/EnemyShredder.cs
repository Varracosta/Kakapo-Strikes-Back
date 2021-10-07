using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShredder : MonoBehaviour
{
    public delegate void OnMissedEnemy();
    public static event OnMissedEnemy MissedEnemy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            MissedEnemy?.Invoke();
        }

        if (other.gameObject.CompareTag("Friendly"))
            Destroy(other.gameObject);
    }
}
