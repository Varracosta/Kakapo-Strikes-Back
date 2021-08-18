using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 RawMovementInput;
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool AttackInput { get; private set; }
    
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y; 
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpInput = true;
        }
    }

    public void StopJump() => JumpInput = false;
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AttackInput = true;
        }
    }
    public void StopAttack() => AttackInput = false;
}
