using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementsManager : MonoBehaviour
{
    #region Main Data
    [Header("Inspector Data")]
    [SerializeField] internal Kakapo kakapo;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsSurface;
    #endregion
    private bool isGrounded;

    private PlayerInputHandler inputHandler;

    private void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    void Update()
    {
        if (PauseMenu.isPaused) { return; }

        CheckingGround();
        if (kakapo.IsHurt) { return; }
        else
        {
            Run();
            Jump(inputHandler.JumpInput);
            Attack(inputHandler.AttackInput);
            Flip();
        }
    }
    private void CheckingGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, whatIsSurface);
        kakapo.animator.SetBool("OnSurface", isGrounded);
    }

    private void Run()
    {
        if(kakapo.IsHurt == false)
        {
            kakapo.rigidBody.velocity = new Vector2(inputHandler.NormInputX * playerData.movementSpeed, kakapo.rigidBody.velocity.y);
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
                kakapo.rigidBody.velocity = new Vector2(kakapo.rigidBody.velocity.x, playerData.jumpSpeed);
            }
        }
    }
    private void Attack(bool attackInput)
    {
        if(attackInput)
        {
            inputHandler.StopAttack();
            if (Time.time >= playerData.nextAttackTime)
            {
                kakapo.animator.SetTrigger("Attack");
                AudioSource.PlayClipAtPoint(kakapo.legKickSFX, Camera.main.transform.position, 0.2f);

                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, playerData.attackRadius, LayerMask.GetMask("Killable enemy"));

                foreach (Collider2D enemy in enemies)
                {
                    enemy.GetComponentInChildren<EnemyHP>().TakeDamage(playerData.damage);
                }
                playerData.nextAttackTime = Time.time + 1f / playerData.attackRate;
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
