using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movements")]
    public float speed = 10f;
    public float jumpSpeed = 25f;
    public float climbingSpeed = 0.25f;
}
