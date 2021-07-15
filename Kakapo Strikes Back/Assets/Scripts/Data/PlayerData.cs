using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movements")]
    public float speed = 10f;
    public float jumpSpeed = 25f;
    public float climbingSpeed = 0.05f;

    [Header("Attack Info")]
    public float attackRate = 2f;
    public float nextAttackTime = 0f;
    public float attackRadius = 0.4f;
    public int damage = 5;

    [Header("Other Info")]
    public float groundCheckRadius = 0.5f;
}
