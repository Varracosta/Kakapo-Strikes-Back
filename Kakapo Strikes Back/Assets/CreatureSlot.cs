using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatureSlot : MonoBehaviour
{
    [SerializeField] private CreatureObject creatureObj;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI text;

    private void OnEnable()
    {
        //GameScoreStats.instance.creaturesStoring.creatureFound += ShowIfMatch;
        GameScoreStats.instance.GetCreatureList().creatureFound += DisplayIfMatch;
    }
    private void DisplayIfMatch(CreatureObject creature)
    {
        if(creature.Id == creatureObj.Id)
        {
            text.text = creature.Name.ToString();
            icon.color = Color.white;
        }
    }
}
