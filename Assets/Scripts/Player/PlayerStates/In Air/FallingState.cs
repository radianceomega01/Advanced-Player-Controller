using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : GroundedState
{
    LayerMask layerMask;
    Collider[] colliders;

    public FallingState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        layerMask = 1 << 3;
        player.SetAnimation("Fall");
    }

    public override void PhysicsProcess()
    {
        colliders = Physics.OverlapSphere(player.GetFootPos(), 0.1f, layerMask);
        if (colliders.Length > 0)
        {
            player.SetState(StateFactory.GetIdleState(player));
        }
            
    }

    public override void Process()
    {
        
    }
}
