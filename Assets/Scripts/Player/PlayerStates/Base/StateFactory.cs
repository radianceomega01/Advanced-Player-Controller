
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class StateFactory
{
    static List<BaseMovementState> playerStateList = new();
    public static BaseMovementState GetPlayerState(Type stateType, PlayerMovement player)
    {
        for (int i = 0; i < playerStateList.Count; i++)
        {
            if (playerStateList[i].GetType() == stateType)
            {
                Debug.Log(playerStateList[i]);
                return (playerStateList[i]);
            }
        }

        BaseMovementState newState = (BaseMovementState)Activator.CreateInstance(stateType, args: player);
        playerStateList.Add(newState);
        Debug.Log(newState);
        return newState;
    }
    public static void GetGroundedStateBasedOnMovementInputType(PlayerMovement player)
    {
        if (player.MovementInputType == MovementInputType.Sprinting)
            player.ChangeState(StateFactory.GetPlayerState(typeof(RunningState), player));
        else if (player.MovementInputType == MovementInputType.Moving)
            player.ChangeState(StateFactory.GetPlayerState(typeof(WalkingState), player));
        else
            player.ChangeState(StateFactory.GetPlayerState(typeof(IdleState), player));
    }

}
