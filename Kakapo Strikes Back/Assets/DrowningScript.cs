using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowningScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Kakapo"))
        {
            FindObjectOfType<SceneLoader>().GameOver();
        }
    }
}
