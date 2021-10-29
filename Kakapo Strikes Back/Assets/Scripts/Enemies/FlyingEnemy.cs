using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for enemies of Level 4. Responsible for checking what hits an enemy: bullet or player. 
//Also stores info abot points for killing, sound, and runs explosion animation
public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private AudioClip popSFX;
    private int pointsPerKill = 100;
    private Animator anim;

    public delegate void OnFlyingEnemyKill();
    public static event OnFlyingEnemyKill FlyingEnemyKill;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(WaitAndDie());
            FlyingEnemyKill?.Invoke();
            Destroy(other.gameObject);
            FindObjectOfType<GameScoreStats>().AddToScore(pointsPerKill);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitAndDie());
            FlyingEnemyKill?.Invoke();
            FindObjectOfType<LivesManager>().DecreaseLives(1);
            FindObjectOfType<GameScoreStats>().AddToScore(pointsPerKill);
        }
    }

    private IEnumerator WaitAndDie()
    {
        AudioSource.PlayClipAtPoint(popSFX, transform.position);
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
