using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private enum LadderPart { complete, top, bottom };
    [SerializeField] private LadderPart ladderPart = LadderPart.complete;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Kakapo kakapo = other.GetComponent<Kakapo>();
        if (kakapo)
        {
            switch (ladderPart)
            {
                case LadderPart.complete:
                    kakapo.canClimb = true;
                    kakapo.ladder = this;
                    break;
                case LadderPart.top:
                    kakapo.topLadder = true;
                    break;
                case LadderPart.bottom:
                    kakapo.bottomLadder = true;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Kakapo kakapo = other.GetComponent<Kakapo>();
        if (kakapo)
        {
            switch (ladderPart)
            {
                case LadderPart.complete:
                    kakapo.canClimb = false;
                    break;
                case LadderPart.top:
                    kakapo.topLadder = false;
                    break;
                case LadderPart.bottom:
                    kakapo.bottomLadder = false;
                    break;
                default:
                    break;
            }
        }
    }
}
