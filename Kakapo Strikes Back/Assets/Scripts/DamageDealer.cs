﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private int currentHealth = 5;
    [SerializeField] private int pointsPerKill = 100;

    private Animator animator;
    public bool IsDead = false;
    private void Start()
    {
        animator = GetComponent<Animator>();

    }
    public int GetDamage() { return damage; }
    public int GetHealth() { return currentHealth; }
    public void TakeDamage(int damageValue)
    {
        currentHealth -= damageValue;

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        FindObjectOfType<UIManager>().AddToScore(pointsPerKill);
        IsDead = true;
        animator.SetBool("IsDead", true);
        StartCoroutine(Dying());
    }

    private IEnumerator Dying()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
