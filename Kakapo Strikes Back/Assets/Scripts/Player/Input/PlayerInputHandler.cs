using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Debug.Log("Move input");
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        Debug.Log("Jump input");
    }
}
