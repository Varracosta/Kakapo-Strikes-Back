using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Kakapo"))
        {
            cutscene.SetActive(true);
        }
    }
}
