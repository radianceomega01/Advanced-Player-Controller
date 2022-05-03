using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerActions playerActions;
    PlayerState state;
    Animator animator;
    Rigidbody rigidBody;

    void Awake()
    {
        playerActions = new PlayerActions();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void Start()
    {
        ChangeState(new IdleState(this));
    }
    private void FixedUpdate()
    {
        state.PhysicsProcess();
    }

    void Update()
    {
        state.Process();
    }

    public void ChangeState(PlayerState state)
    {
        this.state = state;
        state.OnEnter();
    }

    public void SetAnimation()
    {

    }

    public PlayerActions GetPlayerActions() => playerActions;

    public Rigidbody GetRigidBody() => rigidBody;

    private void OnDisable()
    {
        playerActions.Disable();
    }
}
