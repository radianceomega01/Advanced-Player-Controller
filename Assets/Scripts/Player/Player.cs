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
        SetState(new IdleState(this));
    }
    private void FixedUpdate()
    {
        state.PhysicsProcess();
    }

    void Update()
    {
        state.Process();
    }

    public void SetState(PlayerState state)
    {
        this.state = state;
        state.OnEnter();
    }

    public void SetAnimation(string name) =>animator.SetTrigger(name);
    public void SetAnimation(string name, bool value) =>animator.SetBool(name, value);
    public void SetAnimation(string name, float value) =>animator.SetFloat(name, value);

    public PlayerActions GetPlayerActions() => playerActions;

    public Rigidbody GetRigidBody() => rigidBody;

    private void OnDisable()
    {
        playerActions.Disable();
    }
}
