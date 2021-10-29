using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A simple script for Player movement on Level 4
public class PlayerFlying : MonoBehaviour
{
    [SerializeField ]private float speed;
    private Rigidbody2D rigidBody;
    private Vector2 playerDirection;
    private PlayerInputHandler inputHandler;

    private const float MAX_Y = 4f;
    private const float MIN_Y = -3f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }
    void Update()
    {
        playerDirection = new Vector2(0, inputHandler.NormInputY);
    }

    private void FixedUpdate()
    {
        float newYPosition = playerDirection.y * speed;
        Vector2 playerNewPosition = new Vector2(0, newYPosition);
        playerNewPosition.y = Mathf.Clamp(newYPosition, MIN_Y, MAX_Y);
        rigidBody.velocity = playerNewPosition;
    }
}
