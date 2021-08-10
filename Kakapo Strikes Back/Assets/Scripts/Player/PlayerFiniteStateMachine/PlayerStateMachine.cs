using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }  //Empty

    public void Initialize(PlayerState startingState)  //stState - Idle
    {
        CurrentState = startingState;   //Empty(CurrState) -> Idle
        CurrentState.Enter();           //Idle.Enter()
    }

    public void ChangeState(PlayerState newState) //newState - Running
    {
        CurrentState.Exit();            //Idle.Exit()
        CurrentState = newState;        //currState -> Running
        CurrentState.Enter();           //Running.Enter()
    }
}
