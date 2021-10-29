using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Creature Slot in Found Creatures Board (in Pause Menu). Holds original creature information. 
//The script checks if the Player has found a creature, and if he has, - displays icon and name in the Board. 
public class CreatureSlot : MonoBehaviour
{
    [SerializeField] private CreatureObject creatureObj;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CreaturesStoringObject creatureList;

    private void Update()
    {
        if (creatureList.HasCreature(creatureObj))
            DisplayIfMatch(creatureObj);
    }
    private void DisplayIfMatch(CreatureObject creature)
    {
        text.text = creature.Name.ToString();
        icon.color = Color.white;
    }
}
