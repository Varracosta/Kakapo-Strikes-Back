using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerAbilityState
{
    private int xInput;
    private bool isGrounded;
    private bool isTouchingLadder;
    private bool grabInput;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfIsGrounded();
        isTouchingLadder = player.CheckIfIsLadder();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormalizedInputX;
        grabInput = player.InputHandler.GrabInput;

        if(isGrounded && player.CurrentVelocity.y < Mathf.Epsilon)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (isTouchingLadder && grabInput)
        {
            stateMachine.ChangeState(player.GrabLadderState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);

            player.Animator.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Animator.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
