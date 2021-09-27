using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private Animator imageAnim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Kirov") ||
                other.gameObject.CompareTag("Kakapo"))
        {
            Debug.Log("Kirov arrived");
            imageAnim.SetTrigger("FadeOut");
        }
    }
}
