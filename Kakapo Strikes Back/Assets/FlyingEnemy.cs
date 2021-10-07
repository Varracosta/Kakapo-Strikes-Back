using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
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
    }

    private IEnumerator WaitAndDie()
    {
        AudioSource.PlayClipAtPoint(popSFX, transform.position);
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
