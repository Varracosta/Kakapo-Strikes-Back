using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
