using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    idle, 
    running,
    jumping,
    falling,
    climbing,
    hurt,
    alive, 
}
public class Kakapo : MonoBehaviour
{
    //Configuration
    [Header("Main info")] 
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpSpeed = 20f;
    [SerializeField] private BoxCollider2D stompBox;
    public float maxLives = 3f;

    [Header("Audio")]
    [SerializeField] private AudioClip legKickSFX;

    private int _health;
    private float _attackRadius = 0.4f;
    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 _startPosition;
    private int _damage = 5;

    //Climbing info
    public bool canClimb;
    public bool bottomLadder = false;
    public bool topLadder = false;
    public Ladder ladder;
    private float climbSpeed = 3f;

    //Caching references
    private Rigidbody2D rigidBody;
    private CapsuleCollider2D feetCollider;
    private Animator animator;
    private SceneLoader _sceneLoader;
    private LivesManager _livesManager;
    private float startingGravity;
    private State state;

    private float attackRate = 2f;
    private float nextAttackTime = 0f;

    private void Start()
    {
        _startPosition = new Vector3(-16.64f, -2.07f, 1.9f);
        rigidBody = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _livesManager = FindObjectOfType<LivesManager>();
        _health = FindObjectOfType<LivesManager>().GetLives();
        startingGravity = rigidBody.gravityScale;
        state = State.alive;
    }

    private void FixedUpdate()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
    }
    private void Update()
    {
        if(state != State.hurt)
        {


            Attack();
            Run();
            Jump();
            FlipTheCharacter();
            IsTouchingWater();
        }
    }
    private void StateManager()
    {
        if (Math.Abs(rigidBody.velocity.x) > Mathf.Epsilon)
        {
            state = State.running;
        }
        else if (state == State.jumping)
        {

        }
        if(rigidBody.velocity.y < .1f)
        {
            state = State.falling;
        }
        else if(state == State.falling)
        {
            if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                state = State.idle;
            }
        }
        else
        {
            state = State.idle;
        }
    }
    private void Run()
    {
        rigidBody.velocity = new Vector2(horizontalMovement * speed, rigidBody.velocity.y);

        bool isMovingHorizontaly = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Running", isMovingHorizontaly);
    }
    private void Jump() 
    {
        bool isGrounded = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidBody.velocity = new Vector2(0f, jumpSpeed);  //<-- perform jump if the button is pressed and the character is grounded 
        }

        animator.SetBool("Jumping", !isGrounded);

        if(rigidBody.velocity.y < Mathf.Epsilon)
        {
            animator.SetBool("Falling", true);
            if (isGrounded)
                animator.SetBool("Falling", false);
        }
    }
    private void Climb()
    {
        //bool isTouchingLadder = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
        //if (!isTouchingLadder)
        //{
        //    animator.SetBool("Climbing", false);
        //    rigidBody.gravityScale = _startingGravity;
        //    return;
        //}

        //float climbing = Input.GetAxisRaw("Vertical");
        //rigidBody.velocity += new Vector2(rigidBody.velocity.x, climbing * 0.25f);
        //rigidBody.gravityScale = 0f;

        //bool isMovingVertically = Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;
        //animator.SetBool("Climbing", isMovingVertically);
    } 
    private void FlipTheCharacter()
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
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 10f);
        }
    }
    public void TakeDamage(int damageValue)
    {
        state = State.hurt;
        animator.SetBool("Take damage", true);
        rigidBody.velocity = new Vector2(-2f, 10f);  //<-- perform a kickback when kakapo is hurt
        StartCoroutine(GetHurt());
        _livesManager.numberOfLives -= damageValue;
        _livesManager.DisplayLives(_livesManager.numberOfLives);

        if (_livesManager.numberOfLives <= 0)
            Respawn();
    }
    IEnumerator GetHurt()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Take damage", false);
        state = State.alive;
    }
    private void Respawn()
    {
        _livesManager.Respawn();
        FindObjectOfType<UIManager>().ResetScore();
        transform.position = _startPosition;
    }
}
