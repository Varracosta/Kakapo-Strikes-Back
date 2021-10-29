using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I've separated enemy scripts in three for Player to being able to stomp enemies. 

//Script is responsible for moving an enemy, checking if the enemy has reached an edge or a wall, 
//and flipping it and it's sprite
enum IsFacing
{
    Right,
    Left
}
public class Enemy : MonoBehaviour
{
    //configuration
    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform castPos;
    [SerializeField] private EnemyHP enemyHP;
    private float baseCastDist = 0.5f;
    private IsFacing facingDirection;

    //caching references
    private Rigidbody2D rigidBody;
    private Vector3 baseScale;

    private void Start()
    {
        facingDirection = IsFacing.Right;
        rigidBody = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        if (enemyHP.IsDead == false)
        {
            Move();
        }
    }
    private void Move()
    {
        //caching speed in a variable
        float movementSpeed = speed;

        //...and changing it to opposite if enemy is facing left
        if (facingDirection == IsFacing.Left)
            movementSpeed = -speed;

        rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);

        //if enemy is hitting a wall or has reached an edge - flip the enemy and make 
        if (IsHittingWall() || IsAtTheEdge())
        {
            if (facingDirection == IsFacing.Left)
            {
                FlipTheEnemy(IsFacing.Right);
            }
            else
            {
                FlipTheEnemy(IsFacing.Left);
            }
        }
    }
    private void FlipTheEnemy(IsFacing direction)
    {
        Vector3 newScale = baseScale;

        if (direction == IsFacing.Left)
        {
            newScale.x = -baseScale.x;
        }
        else if (direction == IsFacing.Right)
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;
        facingDirection = direction;
    }
    private bool IsHittingWall()
    {
        bool val = false;

        float castDist = baseCastDist;
        if (facingDirection == IsFacing.Left)
        {
            castDist = -baseCastDist;
        }

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground"))
            || Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Platform")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }
    private bool IsAtTheEdge()
    {
        bool val = true;

        float castDist = baseCastDist;

        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground"))
            || Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Platform")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }
}
