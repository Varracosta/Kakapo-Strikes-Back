using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingLadder : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _startingGravity;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _startingGravity = _rigidbody.gravityScale;
    }

    private void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _rigidbody.gravityScale = 0;
            Climb();
            _animator.SetBool("Climbing", true);
        }
    }

    private void Climb()
    {
        float verticalMove = Input.GetAxis("Vertical");
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, verticalMove * 5f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _rigidbody.gravityScale = _startingGravity;
            _animator.SetBool("Climbing", false);
        }
    }
}
