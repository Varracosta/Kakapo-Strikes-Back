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

    private PlayerInputHandler inputHandler;

    private void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckingGround();

        if(kakapo.IsHurt == false)
        {
            Run();
            Jump(inputHandler.JumpInput);
            Attack(inputHandler.AttackInput);
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
        if(kakapo.IsHurt == false)
        {
            kakapo.rigidBody.velocity = new Vector2
                (inputHandler.NormInputX * speed, kakapo.rigidBody.velocity.y);
        }

        kakapo.animator.SetFloat("VelocityX", Mathf.Abs(inputHandler.NormInputX));
    }

    private void Jump(bool jumpInput)
    {
        if(jumpInput)
        {
            inputHandler.StopJump();
            if (isGrounded && !FindObjectOfType<PlayerClimbingLadder>().OnLadder)
            {
                kakapo.rigidBody.velocity = new Vector2(kakapo.rigidBody.velocity.x, jumpSpeed);
            }
        }
    }
    private void Attack(bool attackInput)
    {
        if(attackInput)
        {
            inputHandler.StopAttack();
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
    }
    private void Flip()
    {
        bool isFacingRight = Mathf.Abs(kakapo.rigidBody.velocity.x) > Mathf.Epsilon;
        if (isFacingRight)
            transform.localScale = new Vector2(Mathf.Sign(kakapo.rigidBody.velocity.x), 1f);
    }
}
