using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWhenFound : MonoBehaviour
{
    private Image image;
    private CreaturesStoringObject creatureStore;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        //IsItSprite(GameScoreStats.instance.GetCreatureList());
    }

    private void IsItSprite(List<GameObject> creatureList)
    {
        foreach (GameObject creature in creatureList)
        {
            if(creature.GetComponent<SpriteRenderer>().sprite == image.sprite)
            {
                image.color = Color.white;
            }
        }
    }
}
