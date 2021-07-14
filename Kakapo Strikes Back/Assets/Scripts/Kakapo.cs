using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Kakapo : MonoBehaviour
{
    //Configuration
    [Header("Main info")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private BoxCollider2D stompBox;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    private bool isHurt = false;

    [Header("Audio")]
    [SerializeField] private AudioClip legKickSFX;

    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 startPosition;
    private int health;

    private float groundCheckRadius = 0.5f;

    //Climbing info
    private float climbSpeed = 3f;

    //Caching references
    private Rigidbody2D rigidBody;
    private CircleCollider2D feetCollider;
    private Animator animator;
    private SceneLoader sceneLoader;
    private LivesManager livesManager;
    private float startingGravity;

    private bool isGrounded = true;

    private void Start()
    {
        startPosition = new Vector3(-16.64f, -2.07f, 1.9f);
        rigidBody = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<CircleCollider2D>();
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
        if(!isHurt)
        {
            Attack();
            Run();
            Jump();
            Climb();
            Flip();
            IsTouchingWater();
        }

        FinishTheLevel();
    }
    private void Run()
    { 
        rigidBody.velocity = new Vector2(horizontalMovement * playerData.speed, rigidBody.velocity.y);

        animator.SetFloat("VelocityX", Mathf.Abs(horizontalMovement));
    }
    private void Jump() 
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        if (Input.GetButtonDown("Jump") && wasGrounded)
        {
            rigidBody.velocity = new Vector2(0f, playerData.jumpSpeed);  //<-- perform jump if the button is pressed and the character is grounded 
            animator.SetBool("Jumping", true);
        }

        Collider2D[] groundCheckColliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
        foreach (Collider2D surface in groundCheckColliders)
        {
            if (surface != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                {
                    animator.SetBool("Jumping", false);
                }
            }
        }
    }
    private void Climb()
    {
        bool isTouchingLadder = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
        bool isMovingVertically = Mathf.Abs(verticalMovement) > Mathf.Epsilon;

        if (!isTouchingLadder)
        {
            animator.SetBool("Climbing", false);
            rigidBody.gravityScale = startingGravity;
            return;
        }

        if (isTouchingLadder & isMovingVertically)
        {

            if (isMovingVertically)
            {
                rigidBody.gravityScale = 0f;
                rigidBody.velocity += new Vector2(rigidBody.velocity.x, verticalMovement * playerData.climbingSpeed);
                animator.SetBool("Climbing", true);
                animator.speed = 1f;
            }
            else
            {
                animator.speed = 0f;
            }
        }
    } 
    private void Flip()
    {
        bool isFacingRight = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
        if (isFacingRight)
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), 1f);
    }
    private void Attack()
    {
        if (Time.time >= playerData.nextAttackTime)
        {
            if (Input.GetKey(KeyCode.R))
            {
                animator.SetTrigger("Attack");
                AudioSource.PlayClipAtPoint(legKickSFX, Camera.main.transform.position, 0.2f);

                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position,
                                                                playerData.attackRadius,
                                                                LayerMask.GetMask("Killable enemy"));

                foreach (Collider2D enemy in enemies)
                {
                    enemy.GetComponent<DamageDealer>().TakeDamage(playerData.damage);
                }
                playerData.nextAttackTime = Time.time + 1f / playerData.attackRate;
            }
        }
    }
    private void IsTouchingWater()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            TakeDamage(health);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
       if (other.gameObject.CompareTag("Enemy") || 
           other.gameObject.CompareTag("Spikes"))
       {
           TakeDamage(other.gameObject.GetComponent<DamageDealer>().GetDamage());
       }     
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spikes"))
            return;

        if (stompBox.IsTouchingLayers(LayerMask.GetMask("Killable enemy")))
        {
            other.GetComponent<DamageDealer>().TakeDamage(playerData.damage);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 15f);
        }
    }
    public void TakeDamage(int damageValue)
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        animator.SetBool("Take damage", true);
        rigidBody.velocity += new Vector2(-2.5f, 0f);  //<-- perform a kickback when kakapo is hurt
        livesManager.numberOfLives -= damageValue;
        livesManager.DisplayLives(livesManager.numberOfLives);
        StartCoroutine(GetHurt());

        if (livesManager.numberOfLives == 0)
            sceneLoader.GameOver();
    }
    IEnumerator GetHurt()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Take damage", false);
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
    private void Respawn()
    {
        livesManager.Respawn();
        FindObjectOfType<UIManager>().ResetScore();
        transform.position = startPosition;
    }
    private void FinishTheLevel()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Exit")))
        {
            sceneLoader.LoadNextLevel();
        }
    }
}
