using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Text, that pops up, self-destroys after 1 sec
public class PopUpText : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1f);
    }
}
