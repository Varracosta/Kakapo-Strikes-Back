using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable Object for all creatures that can be found (except for pests) and then added to Found creatures board.
//Holds Id, Name, and Image  
[CreateAssetMenu(fileName = "New Creature", menuName = "Creatures Display/Creatures ")]
public class CreatureObject : ScriptableObject
{
    public int Id;
    public string Name;
    public Sprite Image;
}
