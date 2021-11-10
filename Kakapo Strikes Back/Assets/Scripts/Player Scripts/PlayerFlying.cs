using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A simple script for Player movement on Level 4
public class PlayerFlying : MonoBehaviour
{
    [SerializeField ]private float speed;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Camera gameCamera;
    private PlayerInputHandler inputHandler;
    private CapsuleCollider2D capsuleCollider;

    //variables for boundaries on Y axis, beyond them Kirov won't fly
    private const float MAX_Y = 5f;
    private const float MIN_Y = -5f;

    void Start()
    {
        sceneLoader.SaveScene();
        inputHandler = GetComponent<PlayerInputHandler>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    private void OnEnable()
    {
        KillToPassDisplay.lowKillValue += SwitchOffCollider;
    }

    private void FixedUpdate()
    {
        var deltaY = inputHandler.NormInputY * Time.deltaTime * speed;
        float newYPosition = transform.position.y + deltaY;
        Vector2 kirovNewPosition = new Vector2(transform.position.x, newYPosition);
        kirovNewPosition.y = Mathf.Clamp(newYPosition, MIN_Y, MAX_Y); //"setting up" boundaries
        transform.position = kirovNewPosition;
    }

    // I switch off Kirov's collider so it won't accidentaly hit kereru or take damage from any remaining flying enemy  
    private void SwitchOffCollider() { capsuleCollider.enabled = false; } 
}
