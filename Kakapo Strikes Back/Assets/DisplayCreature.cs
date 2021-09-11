using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCreature : MonoBehaviour
{
    [SerializeField] private Image[] creaturesToDisplay;
    public static DisplayCreature instance;
    private void Start()
    {
        instance = this;
    }

    public void DisplayFoundCreature(GameObject foundCreature)
    {
        foreach(Image creatureToShowUp in creaturesToDisplay)
        {
            if(foundCreature.GetComponent<SpriteRenderer>().sprite == creatureToShowUp.sprite)
            {
                creatureToShowUp.color = Color.white;
            }
        }
    }
}
