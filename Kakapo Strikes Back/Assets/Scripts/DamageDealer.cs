using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float currentHealth = 5f;
    [SerializeField] private int pointsPerKill = 100;
    [SerializeField] Collider2D collider;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    public int GetDamage() { return _damage; }
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
        rigidbody.velocity = new Vector2(0f, 0f);
        animator.SetBool("IsDead", true);
        
        StartCoroutine(Dying());
    }

    private IEnumerator Dying()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
