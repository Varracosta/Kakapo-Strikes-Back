using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    #region Properties 
    public PlayerStateMachine StateMachine { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    //States
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunningState RunningState { get; private set; }

    //Components
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    //Variables
    private Vector2 workspaceVector;
    #endregion

    private void Awake()
    {
        //Declaring the State machine and initializing player's states 
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        RunningState = new PlayerRunningState(this, StateMachine, playerData, "Run");
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody = GetComponent<Rigidbody2D>();

        //Initializing the State Machine
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = Rigidbody.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetVelocityX(float velocity)
    {
        workspaceVector.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = workspaceVector;
        CurrentVelocity = workspaceVector;
    }
}
