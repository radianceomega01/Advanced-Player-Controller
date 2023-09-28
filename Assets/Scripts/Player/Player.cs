using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform footPos;

    PlayerActions playerActions;
    PlayerState state;
    PlayerState previousState;
    Animator animator;
    Rigidbody rigidBody;

    public event Action OnAnimComplete;

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
        SetState(StateFactory.GetPlayerState(typeof(IdleState), this));
    }
    private void FixedUpdate()
    {
        state.PhysicsProcess();
    }

    void Update()
    {
        state.Process();
    }

    public void SetState(PlayerState newState)
    {
        previousState = state;
        if(state != null)
            state.OnExit();
        state = newState;
        state.OnEnter();
    }

    public PlayerState GetPreviousState() => previousState;

    public void SetAnimation(string name) => animator.Play(name);

    //public void ResetAnimation(string name) => animator.ResetTrigger(name);
    public void SetAnimation(string name, float value) =>animator.SetFloat(name, value);

    public void AnimCompete() => OnAnimComplete?.Invoke();

    public PlayerActions GetPlayerActions() => playerActions;

    public Vector3 GetFootPos() => footPos.position;

    public Rigidbody GetRigidBody() => rigidBody;

    private void OnDisable()
    {
        playerActions.Disable();
    }
}
