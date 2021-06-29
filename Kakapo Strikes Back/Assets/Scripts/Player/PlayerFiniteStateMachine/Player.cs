using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region State variables
    //States
    [SerializeField] private PlayerData playerData;
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunningState RunState { get; private set; }
    public PlayerJumpState JumpState { get; private set;}
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerGrabLadderState GrabLadderState { get; private set; }
    public PlayerClimbLadderState ClimbLadderState { get; private set; }
    #endregion

    #region Components
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    #endregion

    #region Check 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ladderCheck;
    #endregion

    #region Misc
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 workspaceVector;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        //Declaring the State machine and initializing player's states 
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        RunState = new PlayerRunningState(this, StateMachine, playerData, "Run");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "InAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "InAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "Land");
        GrabLadderState = new PlayerGrabLadderState(this, StateMachine, playerData, "LadderGrab");
        ClimbLadderState = new PlayerClimbLadderState(this, StateMachine, playerData, "Climb");
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody = GetComponent<Rigidbody2D>();
        FacingDirection = 1;

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
    #endregion

    #region Seting Up Functions
    public void SetVelocityX(float velocity)
    {
        workspaceVector.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = workspaceVector;
        CurrentVelocity = workspaceVector;
    }

    public void SetVelocityY(float velocity)
    {
        workspaceVector.Set(CurrentVelocity.x, velocity);
        Rigidbody.velocity = workspaceVector;
        CurrentVelocity = workspaceVector;
    }
    #endregion

    #region Check Functions
    public bool CheckIfIsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }
    public bool CheckIfIsLadder()
    {
        return Physics2D.Raycast(ladderCheck.position, Vector2.right * FacingDirection, playerData.ladderCheckDistance, playerData.whatIsLadder);
    }
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    #endregion

    #region Other Functions
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimashionFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
}
