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
    private float climbingSpeed = 3f;

    private Rigidbody2D rb;
    private Animator anim;
    private PlayerInputHandler inputHandler;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    void Update()
    {
        DetectingLadder();
        GrabingLadder();
        Climbing();

        CenteredPositionOnLadder();
        anim.SetFloat("VelocityY", inputHandler.NormInputY);
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
            rb.velocity = new Vector2(rb.velocity.x, inputHandler.NormInputY * climbingSpeed);
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void GrabingLadder()
    {
        if (isLadderDetected && inputHandler.NormInputY != 0)
        {
            OnLadder = true;

        }
        else if (!isLadderDetected)
        {
            OnLadder = false; 
        }

        anim.SetBool("OnLadder", OnLadder);
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
