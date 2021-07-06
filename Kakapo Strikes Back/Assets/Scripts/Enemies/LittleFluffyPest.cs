﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum IsFacing
{
    Right,
    Left
}
public class LittleFluffyPest : Enemy
{
    //configuration
    [SerializeField] private float _maxHealth = 5f;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private Transform castPos;
    private float baseCastDist = 0.5f;
    private IsFacing _facingDirection;

    //caching references
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private Vector3 baseScale;


    private void Start()
    {
        _facingDirection = IsFacing.Right;
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        baseScale = transform.localScale;
    }

    private void Update()
    {

    }
    private void FixedUpdate()
    {
        if(GetComponent<DamageDealer>().IsDead == false)
        {
            Move();
        }
    }
    protected override void Move()
    {

        //caching _speed in a variable
        float movementSpeed = _speed;

        //...and changing it to opposite if enemy is facing left
        if (_facingDirection == IsFacing.Left)
            movementSpeed = -_speed;

        _rigidBody.velocity = new Vector2(movementSpeed, _rigidBody.velocity.y);
       
        //if enemy is hitting a wall or has reached an edge - flip the enemy and make 
        if (IsHittingWall() || IsAtTheEdge())
        {
            if (_facingDirection == IsFacing.Left)
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
        _facingDirection = direction;
    }
    private bool IsHittingWall()
    {
        bool val = false;

        float castDist = baseCastDist;
        if(_facingDirection == IsFacing.Left)
        {
            castDist = -baseCastDist;
        }

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.blue);
        if(Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
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

        Debug.DrawLine(castPos.position, targetPos, Color.red);
        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }

    protected override void Die()
    {
    }
}
