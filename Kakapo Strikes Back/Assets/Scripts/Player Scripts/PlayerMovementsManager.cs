using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckingGround();
        Run();
        Jump();
        Attack();
        Flip();
    }
    private void CheckingGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsSurface);
        kakapo.animator.SetBool("OnSurface", isGrounded);
    }

    private void Run()
    {
        kakapo.rigidBody.velocity = new Vector2
            (kakapo.horizontalMovement * speed, kakapo.rigidBody.velocity.y);

        kakapo.animator.SetFloat("VelocityX", Mathf.Abs(kakapo.horizontalMovement));
    }

    private void Jump()
    {
        if (isGrounded && !FindObjectOfType<PlayerClimbingLadder>().OnLadder)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                kakapo.rigidBody.velocity = new Vector2(kakapo.rigidBody.velocity.x, jumpSpeed);
            }
        }
    }
    private void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKey(KeyCode.R))
            {
                kakapo.animator.SetTrigger("Attack");
                AudioSource.PlayClipAtPoint(kakapo.legKickSFX, Camera.main.transform.position, 0.2f);

                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, LayerMask.GetMask("Killable enemy"));

                foreach (Collider2D enemy in enemies)
                {
                    enemy.GetComponent<DamageDealer>().TakeDamage(kakapo.damage);
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
