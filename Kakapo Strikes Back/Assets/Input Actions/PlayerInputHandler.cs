using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerActionControls playerActionControls;
    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        playerActionControls.Enable();
    }
    private void Update()
    {

    }
}
