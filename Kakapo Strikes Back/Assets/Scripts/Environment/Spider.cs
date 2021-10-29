using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Katipo is so chad that it has it's own script. Works pretty similar as Enemy script, except a spider moves verticaly. 
//Also it damages Player AND enemies equally. You don't mess with katipo, no no. Although, it's not so scary or dangerous in real life
public class Spider : MonoBehaviour
{
    enum IsFacing { up, down };

    [SerializeField] private float _speed = 1f;
    [SerializeField] private Transform castPos;
    [SerializeField] private LayerMask whatIsSurface;
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
    private void Move()
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
        if (Physics2D.Linecast(castPos.position, targetPos, whatIsSurface))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    //Flipping the spider 
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

        if (other.gameObject.CompareTag("HurtBox"))
        {
            other.gameObject.GetComponent<EnemyHP>().Die();
        }
    }
}
