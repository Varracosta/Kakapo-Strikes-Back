using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbingLadder : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsLadder;
    [SerializeField] private Transform ladderCheck;
    private float ladderCheckRadius = 0.1f;
    private bool isLadderDetected;
    public bool OnLadder { get; private set; }
    private bool centered = true;
    private float ladderCenter;

    private float verticalMovement;
    private float climbingSpeed = 2f;

    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DetectingLadder();
        GrabingLadder();
        Climbing();

        LadderUpDown();
        CenteredPositionOnLadder();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(ladderCheck.position, ladderCheckRadius);
    }

    private void DetectingLadder()
    {
        isLadderDetected = Physics2D.OverlapCircle(ladderCheck.position, ladderCheckRadius, whatIsLadder);
    }

    private void Climbing()
    {
        if (OnLadder)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = new Vector2(rb.velocity.x, verticalMovement * climbingSpeed);
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void GrabingLadder()
    {
        if (isLadderDetected && verticalMovement != 0)
        {
            OnLadder = true;

        }
        else if (!isLadderDetected)
        {
            OnLadder = false; 
        }

        anim.SetBool("OnLadder", OnLadder);
    }
    private void LadderUpDown()
    {
        verticalMovement = Input.GetAxisRaw("Vertical");

        anim.SetFloat("VelocityY", verticalMovement);
    }

    private void CenteredPositionOnLadder()
    {
        if (OnLadder && centered)
        {
            centered = !centered;
            LadderCenter();
        }
        else if (!OnLadder && !centered)
            centered = true;
    }

    private void LadderCenter()
    {
        ladderCenter = Physics2D.OverlapCircle(ladderCheck.position, ladderCheckRadius, whatIsLadder).GetComponent<BoxCollider2D>().bounds.center.x;
        transform.position = new Vector2(ladderCenter, transform.position.y);
    }
}
