using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    //Script for dealing damage to player
    [SerializeField] private int damage = 1;

    public int GetDamage() { return damage; }
}
