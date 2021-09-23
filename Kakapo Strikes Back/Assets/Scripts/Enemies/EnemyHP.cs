using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
     //Script responsible for taking damage from Player

    [SerializeField] private AudioClip deathPopSFX;
    private Animator animator;
    private int enemyHP = 5;
    private int currentHP;
    private int pointsPerKill = 100;
    public bool IsDead { get; private set; }

    public delegate void OnEnemyKill();
    public static  event OnEnemyKill EnemyKill;


    void Start() 
    {
        IsDead = false;
        currentHP = enemyHP;
        animator = transform.parent.GetComponent<Animator>();
    }

    public void TakeDamage(int damageValue)
    {
        currentHP -= damageValue;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        IsDead = true;
        EnemyKill?.Invoke();
        KillQuotes.instance.PlayKillPhrase();
        FindObjectOfType<GameScoreStats>().AddToScore(pointsPerKill);
        AudioSource.PlayClipAtPoint(deathPopSFX, Camera.main.transform.position, 10f);

        StartCoroutine(Dying());
    }

    public IEnumerator Dying()
    {
        animator.SetBool("IsDead", IsDead);
        yield return new WaitForSeconds(0.3f);
        Destroy(transform.parent.gameObject);
    }
}
