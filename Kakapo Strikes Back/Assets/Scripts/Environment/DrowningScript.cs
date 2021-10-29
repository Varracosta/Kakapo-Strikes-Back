using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script sits on water grid. If Player hits the water, script starts "dying" (I hate this) process for Player
public class DrowningScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Kakapo"))
        {
            StartCoroutine(FindObjectOfType<Kakapo>().Dying());
        }
    }
}
