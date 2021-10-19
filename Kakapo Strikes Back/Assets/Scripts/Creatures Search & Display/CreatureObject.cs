using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Creature", menuName = "Creatures Display/Creatures ")]
public class CreatureObject : ScriptableObject
{
    public int Id;
    public string Name;
    public Sprite Image;
}
