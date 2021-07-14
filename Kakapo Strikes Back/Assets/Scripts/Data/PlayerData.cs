using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Main Info")]
    public int lives = 3;

    [Header("Movements")]
    public float speed = 10f;
    public float jumpSpeed = 25f;
    public float climbingSpeed = 0.25f;

    [Header("Attack Info")]
    public float attackRate = 2f;
    public float nextAttackTime = 0f;
    public float attackRadius = 0.4f;
    public int damage = 5;
}
