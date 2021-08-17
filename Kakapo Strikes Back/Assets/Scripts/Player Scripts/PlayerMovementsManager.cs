using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementsManager : MonoBehaviour
{
    #region Main Data
    [Header("Main Data")]
    [SerializeField] internal Kakapo kakapo;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsSurface;
    #endregion

    private bool isGrounded;
    private float speed = 10f;
    private float jumpSpeed = 20f;
    private float attackRadius = 0.4f;
    private float nextAttackTime = 0f;
    private float attackRate = 2f;
    private float groundCheckRadius = 0.5f;

    private PlayerActionControls playerActionControls;
    public Vector2 Movement { get; private set; }

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        playerActionControls.Enable();
    }

    private void Start()
    {
        playerActionControls.Player.Jump.performed += Jump;
        playerActionControls.Player.Attack.performed += Attack;
    }

    // Update is called once per frame
    void Update()
    {
        Movement = playerActionControls.Player.Movement.ReadValue<Vector2>();
        CheckingGround();

        if(kakapo.IsHurt == false)
        {
            Run();
            //Jump();
            //Attack();
            Flip();
        }
    }
    private void CheckingGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsSurface);
        kakapo.animator.SetBool("OnSurface", isGrounded);
    }

    private void Run()
    {
        kakapo.rigidBody.velocity = new Vector2
            (Movement.x * speed, kakapo.rigidBody.velocity.y);

        kakapo.animator.SetFloat("VelocityX", Mathf.Abs(Movement.x));
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && !FindObjectOfType<PlayerClimbingLadder>().OnLadder)
        {
            kakapo.rigidBody.velocity = new Vector2(kakapo.rigidBody.velocity.x, jumpSpeed);
        }
    }
    private void Attack(InputAction.CallbackContext context)
    {
        if (Time.time >= nextAttackTime)
        {
             kakapo.animator.SetTrigger("Attack");
             AudioSource.PlayClipAtPoint(kakapo.legKickSFX, Camera.main.transform.position, 0.2f);

             Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, LayerMask.GetMask("Killable enemy"));

             foreach (Collider2D enemy in enemies)
             {
                 enemy.GetComponentInChildren<EnemyHP>().TakeDamage(kakapo.damage);
             }
             nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    private void Flip()
    {
        bool isFacingRight = Mathf.Abs(kakapo.rigidBody.velocity.x) > Mathf.Epsilon;
        if (isFacingRight)
            transform.localScale = new Vector2(Mathf.Sign(kakapo.rigidBody.velocity.x), 1f);
    }
}
