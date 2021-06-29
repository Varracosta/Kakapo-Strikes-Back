using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbLadderState : PlayerTouchingLadderState
{
    public PlayerClimbLadderState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityY(playerData.ladderClimbVelocity);

        if(yInput != 1)
        {
            stateMachine.ChangeState(player.GrabLadderState);
        }
    }
}
