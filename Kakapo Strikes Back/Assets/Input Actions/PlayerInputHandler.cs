using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 rawMovementInput; 
    public int NormInputX { get; private set; }  
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool AttackInput { get; private set; }
    public Vector2 MousePosition { get; private set; }
    public bool ShootInput { get; private set; }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();
 
        NormInputX = (int)(rawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(rawMovementInput * Vector2.up).normalized.y;
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
    public void OnMousePositionInput(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }
    public void OnShootInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShootInput = true;
        }
    }
    public void StopShooting() => ShootInput = false;
}
