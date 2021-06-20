using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }
    //Entering the State
    public virtual void Enter()
    {
        DoChecks();
        player.Animator.SetBool(animBoolName, true);
        startTime = Time.time;
    }
    //Exiting the State
    public virtual void Exit()
    {
        player.Animator.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }
    //Substitute for FixedUpdate?
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    //Checking for necessary conditions (e.g. ground check, ladder check and etc.)
    public virtual void DoChecks()
    {

    }
}
