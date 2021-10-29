using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script responsible for dealing damage to player
public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    public int GetDamage() { return damage; }
}
