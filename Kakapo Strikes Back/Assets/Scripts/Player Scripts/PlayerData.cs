using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A scriptable object for storing all necessary data for Player
[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Speed")]
    public float movementSpeed = 10f;
    public float jumpSpeed = 21f;
    public float climbSpeed = 3f;

    [Header("Attack info")]
    public float attackRadius = 0.4f;
    public float nextAttackTime = 0f;
    public float attackRate = 2f;
    public int damage = 5;

    [Header("Other info")]
    public float groundCheckRadius = 0.5f;
    public float ladderCheckRadius = 0.1f;
    public Vector3 startingPosition;
    public float startingGravity;
}
