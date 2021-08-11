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
    #endregion
   
    #region Physics variables
    internal float horizontalMovement;
    internal float verticalMovement;
    internal Vector3 startPosition;
    internal float startingGravity;
    #endregion

    public int health;
    public bool isHurt = false;
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
        startPosition = new Vector3(-16.64f, -2.07f, 1.9f);
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        livesManager = FindObjectOfType<LivesManager>();
        health = FindObjectOfType<LivesManager>().GetLives();
        startingGravity = rigidBody.gravityScale;
    }

    private void FixedUpdate()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
    }
    private void Update()
    {
        FinishTheLevel();
    }

    public void TakeDamage(int damageValue)
    {
        isHurt = true;
        //rigidBody.velocity = new Vector2(0f, 15f);
        //Physics2D.IgnoreLayerCollision(10, 11, true);
        AudioSource.PlayClipAtPoint(hurtSFX, Camera.main.transform.position, 5f);
        animator.SetBool("Take damage", true);

        livesManager.numberOfLives -= damageValue;
        livesManager.DisplayLives(livesManager.numberOfLives);

        StartCoroutine(GetHurt());

        if (livesManager.numberOfLives == 0)
        {
            sceneLoader.GameOver();
        }
    }
    IEnumerator GetHurt()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Take damage", false);
        isHurt = false;
        //Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    private void FinishTheLevel()
    {
        if (stompBox.IsTouchingLayers(LayerMask.GetMask("Exit")))
        {
            sceneLoader.LoadNextLevel();
        }
    }
}
