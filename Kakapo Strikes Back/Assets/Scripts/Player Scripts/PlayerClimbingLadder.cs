using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Responsible for Player climbing a ladder.
public class PlayerClimbingLadder : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private LayerMask whatIsLadder;
    [SerializeField] private Transform ladderCheck;
    private float ladderCheckRadius = 0.1f;
    private bool isLadderDetected;
    public bool OnLadder { get; private set; }
    private bool centered = true;
    private float ladderCenter;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private PlayerInputHandler inputHandler;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    void Update()
    {
        DetectingLadder();
        GrabingLadder();
        Climbing();

        CenteredPositionOnLadder();
        animator.SetFloat("VelocityY", inputHandler.NormInputY);
    }
    private void DetectingLadder()
    {
        isLadderDetected = Physics2D.OverlapCircle(ladderCheck.position, playerData.ladderCheckRadius, whatIsLadder);
    }

    private void Climbing()
    {
        if (OnLadder)
        {
            rigidBody.bodyType = RigidbodyType2D.Kinematic;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, inputHandler.NormInputY * playerData.climbSpeed);
        }
        else
        {
            rigidBody.bodyType = RigidbodyType2D.Dynamic;
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

        animator.SetBool("OnLadder", OnLadder);
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
