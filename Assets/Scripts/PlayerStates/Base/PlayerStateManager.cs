using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerState state;
    Animator animator;
    public static PlayerStateManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance);
        else
            Instance = this;

        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        ChangeState(new IdleState());
    }

    public void ChangeState(PlayerState state)
    {
        this.state = state;
        state.OnEnter();
    }

    public void SetAnimation()
    { 
    
    }
}
