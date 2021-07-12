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
    public float maxLives = 3f;
    public GameObject explosionAnim;

    private bool isHurt = false;

    [Header("Audio")]
    [SerializeField] private AudioClip legKickSFX;

    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 _startPosition;
    private int _health;

    private float _attackRadius = 0.4f;
    private int _damage = 5;

    private float groundCheckRadius = 0.5f;

    //Climbing info
    private float climbSpeed = 3f;

    //Caching references
    private Rigidbody2D rigidBody;
    private CircleCollider2D feetCollider;
    private Animator animator;
    private SceneLoader _sceneLoader;
    private LivesManager _livesManager;
    private float startingGravity;

    private float attackRate = 2f;
    private float nextAttackTime = 0f;

    private bool isGrounded = true;

    private void Start()
    {
        _startPosition = new Vector3(-16.64f, -2.07f, 1.9f);
        rigidBody = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _livesManager = FindObjectOfType<LivesManager>();
        _health = FindObjectOfType<LivesManager>().GetLives();
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
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKey(KeyCode.R))
            {
                animator.SetTrigger("Attack");
                AudioSource.PlayClipAtPoint(legKickSFX, Camera.main.transform.position, 0.2f);

                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position,
                                                                _attackRadius,
                                                                LayerMask.GetMask("Killable enemy"));

                foreach (Collider2D enemy in enemies)
                {
                    enemy.GetComponent<DamageDealer>().TakeDamage(_damage);
                }
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    private void IsTouchingWater()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            TakeDamage(_health);
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
        if (stompBox.IsTouchingLayers(LayerMask.GetMask("Killable enemy")) 
            && !other.gameObject.CompareTag("Spikes"))
        {
            other.GetComponent<DamageDealer>().TakeDamage(_damage);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 15f);
        }
    }
    public void TakeDamage(int damageValue)
    {
        isHurt = true;
        animator.SetBool("Take damage", true);
        rigidBody.velocity = new Vector2(-2f, 10f);  //<-- perform a kickback when kakapo is hurt
        StartCoroutine(GetHurt());
        _livesManager.numberOfLives -= damageValue;
        _livesManager.DisplayLives(_livesManager.numberOfLives);

        if (_livesManager.numberOfLives <= 0)
            _sceneLoader.GameOver();
    }
    IEnumerator GetHurt()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Take damage", false);
        isHurt = false;
    }
    private void Respawn()
    {
        _livesManager.Respawn();
        FindObjectOfType<UIManager>().ResetScore();
        transform.position = _startPosition;
    }
}
