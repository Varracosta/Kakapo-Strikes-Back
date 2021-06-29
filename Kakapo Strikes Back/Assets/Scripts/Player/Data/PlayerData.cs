using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;

    [Header("Check components")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
    public float ladderCheckDistance = 0.5f;
    public LayerMask whatIsLadder;
    public Transform attackPoint;

    [Header("Ladder Climb State")]
    public float ladderClimbVelocity = 2f;

    [Header("Audio")]
    public AudioClip legKickSFX;
}
