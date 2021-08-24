using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField] internal Kakapo kakapo;
    [SerializeField] private AudioClip levelCompleteSFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        TriggerTakingDamage(other);
        FinishLevel(other);
    }

    private void TriggerTakingDamage(Collider2D other)
    {
        if (kakapo.IsHurt == false)
        {
            if (other.gameObject.CompareTag("Enemy") ||
                other.gameObject.CompareTag("Hedgehog"))
            {
                kakapo.PerformKnockback(other);
                kakapo.TakeDamage(other.gameObject.GetComponent<DamageDealer>().GetDamage());
            }
        }
        else { return; }
    }

    private void FinishLevel(Collider2D other)
    {
        if (other.gameObject.CompareTag("ExitFlag"))
        {
            AudioSource.PlayClipAtPoint(levelCompleteSFX, Camera.main.transform.position, 0.5f);
            SceneLoader.instance.LoadNextLevel();
        }
    }
}
