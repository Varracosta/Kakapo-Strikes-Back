using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelected : MonoBehaviour
{
    public Button primaryButton;
    void Start()
    {
        if (UIManager.instance.IsPauseMenuActive())
        {
            primaryButton.Select();
        }
    }
}
