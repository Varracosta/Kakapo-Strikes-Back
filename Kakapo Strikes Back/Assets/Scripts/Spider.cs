using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spider : Enemy
{
    enum IsFacing { up, down };

    [SerializeField] private float _speed = 1f;
    [SerializeField] private Transform castPos;
    private float baseCastDist = 0.2f;
    private int damage = 1;
    
    private Rigidbody2D _rigidbody;
    private IsFacing _facingDirection;
    private Vector3 baseScale;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _facingDirection = IsFacing.down;
        baseScale = transform.localScale;
    }
    private void FixedUpdate()
    {
        Move();
    }
    protected override void Move()
    {
        float movementSpeed = _speed;

        if (_facingDirection == IsFacing.up)
            _rigidbody.velocity = new Vector2(0, movementSpeed);
        else if(_facingDirection == IsFacing.down)
            _rigidbody.velocity = new Vector2(0, -movementSpeed);


        if (IsHittingGround())
        {
            if (_facingDirection == IsFacing.up)
                FlipTheSpider(IsFacing.down);
            else
                FlipTheSpider(IsFacing.up);
        }
    }


    private bool IsHittingGround()
    {
        bool val = false;

        float castDist = baseCastDist;

        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.red);
        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }
    private void FlipTheSpider(IsFacing direction)
    {
        Vector3 newScale = baseScale;

        if (direction == IsFacing.up)
            newScale.y = -baseScale.y;
        else if (direction == IsFacing.down)
            newScale.y = baseScale.y;

        transform.localScale = newScale;
        _facingDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Kakapo"))
        {
            other.gameObject.GetComponent<Kakapo>().TakeDamage(damage);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<DamageDealer>().TakeDamage(damage);
        }
    }

    protected override void Die()
    {
       //katipo is endangered spider in NZ, it's best to make it unkillable. Just try to avoid it
    }

}
