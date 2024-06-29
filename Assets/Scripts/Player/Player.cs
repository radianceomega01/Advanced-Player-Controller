using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public values to be shared with player states
    [SerializeField] public float walkingSpeed = 100f;
    [SerializeField] public float runningSpeed = 250f;
    [SerializeField] public float jumpForce = 400f;
    [SerializeField] public float slideForce = 5f;
    [SerializeField] Transform footPos;

    [Header("Colliders")]
    [SerializeField] Collider[] playerColliders;

    PlayerActions playerActions;
    PlayerState state;
    PlayerState previousState;
    Animator animator;
    Rigidbody rigidBody;
    LayerMask layerMask = 1 << 3;
    Collider[] colliders;

    public int JumpCount { get; set; }
    public float PreviousYPos { get; private set; }

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

    private void LateUpdate()
    {
        PreviousYPos = transform.position.y;
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
    public void SetAnimationTrigger(string name) => animator.SetTrigger(name);
    public void SetAnimation(string name, float value) =>animator.SetFloat(name, value);

    public void AnimComplete() => OnAnimComplete?.Invoke();

    public PlayerActions GetPlayerActions() => playerActions;

    public Vector3 GetFootPos() => footPos.position;

    public Rigidbody GetRigidBody() => rigidBody;
    public void SetPlayerCollider(int type)
    {
        if (type >= 0 && type < playerColliders.Length)
        {
            playerColliders[type].gameObject.SetActive(true);
            for (int i = 0; i < playerColliders.Length; i++)
            {
                if (i != type)
                    playerColliders[i].gameObject.SetActive(false);
            } 
        }
        else
            return;
    }

    public void CheckAndMoveToFallingState()
    {
        colliders = Physics.OverlapSphere(GetFootPos(), 0.1f, layerMask);
        if (transform.position.y < PreviousYPos && colliders.Length == 0)
            SetState(StateFactory.GetPlayerState(typeof(FallingState), this));
        /*if (colliders.Length == 0)
            SetState(StateFactory.GetPlayerState(typeof(FallingState), this));*/
    }

    public void CheckAndMoveToGroundedState()
    {
        colliders = Physics.OverlapSphere(GetFootPos(), 0.1f, layerMask);
        if (colliders.Length > 0)
        {
            SetState(StateFactory.GetPlayerState(typeof(IdleState), this));
        }
    }
    private void OnDisable()
    {
        playerActions.Disable();
    }
}
