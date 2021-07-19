using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbLadder : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private float climbSpeed = 2f;

    private Rigidbody2D rb;
    private float verticalMovement;
    private float distance;
    public LayerMask whatIsLadder;
    private bool isClimbing;
    private float startingGravity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingGravity = rb.gravityScale;
    }

    private void FixedUpdate()
    {
        verticalMovement = Input.GetAxisRaw("Vertical");

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);

        if(hitInfo.collider != null)
        {
            if(verticalMovement != 0)
            {
                isClimbing = true;
            }
        }
        else if(Input.GetAxisRaw("Horizontal") != 0)
        {
            isClimbing = false;
        }

        if(isClimbing == true && hitInfo.collider != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, verticalMovement * climbSpeed);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = startingGravity;
        }
    }
}
