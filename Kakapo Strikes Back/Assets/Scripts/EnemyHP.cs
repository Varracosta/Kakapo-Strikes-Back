using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    /*
     Script responcible for taking damage from Player
     */
    [SerializeField] private AudioClip deathPopSFX;
    [SerializeField] private Animator animator;
    public int enemyHP = 5;
    private int currentHP;
    private int pointsPerKill = 100;
    public bool IsDead = false;


    void Start()
    {
        currentHP = enemyHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHP <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
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
        FindObjectOfType<UIManager>().AddToScore(pointsPerKill);
        IsDead = true;
        AudioSource.PlayClipAtPoint(deathPopSFX, Camera.main.transform.position, 10f);
        animator.SetBool("IsDead", true);
        StartCoroutine(Dying());
    }

    private IEnumerator Dying()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
