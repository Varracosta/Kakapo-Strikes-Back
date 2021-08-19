using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Kakapo : MonoBehaviour
{
    #region Inspector Part
    [Header("Main References")]
    [SerializeField] internal PlayerMovementsManager playerMovementsManager;
    [SerializeField] internal PlayerCollisionManager playerCollisionManager;

    [Header("Audio")]
    [SerializeField] internal AudioClip legKickSFX;
    [SerializeField] private AudioClip hurtSFX;
    [SerializeField] private AudioClip levelCompleteSFX;
    [SerializeField] private AudioClip gameOverSFX;
    #endregion
   
    #region Physics variables
    internal Vector3 startPosition;
    internal float startingGravity;
    #endregion

    public bool IsHurt { get; private set; }
    internal int damage = 5;

    #region Cached components 
    [SerializeField] internal BoxCollider2D stompBox;
    internal Rigidbody2D rigidBody;
    internal Animator animator;
    private SceneLoader sceneLoader;
    private LivesManager livesManager;

    #endregion
    private void Start()
    {
        IsHurt = false;
        startPosition = new Vector3(-16.64f, -2.07f, 1.9f);
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        livesManager = FindObjectOfType<LivesManager>();
        startingGravity = rigidBody.gravityScale;
    }

    private void Update()
    {
        FinishTheLevel();
    }

    public void TakeDamage(int damageValue)
    {
        IsHurt = true;
        AudioSource.PlayClipAtPoint(hurtSFX, Camera.main.transform.position, 5f);
        livesManager.DecreaseLives(damageValue);
        StartCoroutine(GetHurt());

        if (livesManager.NumberOfLives == 0)
        {
            StartCoroutine(Dying());
        }
    }
    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        animator.SetBool("Take damage", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Take damage", false);
        Physics2D.IgnoreLayerCollision(10, 11, false);
        IsHurt = false;
    }
    public IEnumerator Dying()
    {
        animator.SetBool("Take damage", true);
        Physics2D.IgnoreLayerCollision(10, 11, false);
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position, 2f);
        sceneLoader.GameOver();
    }
    public void PerformKnockback(Collider2D other)
    {
        if(other.gameObject.transform.position.x > transform.position.x)
        {
            rigidBody.velocity = new Vector2(-5f, 15f);
        }
        else if (other.gameObject.transform.position.x < transform.position.x)
        {
            rigidBody.velocity = new Vector2(5f, 15f);
        }
    }

    private void FinishTheLevel()
    {
        if (stompBox.IsTouchingLayers(LayerMask.GetMask("Exit")))
        {
            AudioSource.PlayClipAtPoint(levelCompleteSFX, Camera.main.transform.position);
            sceneLoader.LoadNextLevel();
        }
    }
}
