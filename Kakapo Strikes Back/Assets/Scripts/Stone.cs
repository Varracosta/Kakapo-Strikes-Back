using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private BoxCollider2D stomper;


    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject enemy = other.gameObject;
        if (stomper.IsTouchingLayers(LayerMask.GetMask("Killable enemy")))
        {
            FindObjectOfType<UIManager>().AddToScore(enemy.GetComponent<DamageDealer>().GetPointsPerKill());
            Destroy(enemy);
        }
    }
}
