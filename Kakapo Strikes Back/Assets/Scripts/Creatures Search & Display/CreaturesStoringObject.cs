using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A creature "inventory"/info-holder. Everytime the Player finds a creature, the script checks if the creature has already been founded, 
//and if no - adds it to the list, and activates visial and audio effects. It also contains info about creature count
//to display in Total Score Scene 

[CreateAssetMenu(fileName = "New Creatures Storing", menuName = "Creatures Storing")]
public class CreaturesStoringObject : ScriptableObject
{
    public List<CreatureObject> Container = new List<CreatureObject>();
    private int creatureCount = 0;

    public void AddCreature(CreatureObject _creature, GameObject animal, GameObject player)
    {
        HasCreature(_creature);

        if (!HasCreature(_creature))
        {
            Container.Add(_creature);
            creatureCount++;
            animal.gameObject.GetComponent<FlashWhenFound>().PlayEffects(player);
        }
    }

    public bool HasCreature(CreatureObject _creature)
    {
        bool hasCreature = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if(Container[i].Id == _creature.Id)
            {
                hasCreature = true;
                break;
            }
        }
        return hasCreature;
    }
    public int GetCreatureCount() { return creatureCount;  }
    public void ResetCreatureCount() { creatureCount = 0; }
    public void ClearContainer()
    {
        creatureCount = 0;
        Container.Clear();
    }
}
