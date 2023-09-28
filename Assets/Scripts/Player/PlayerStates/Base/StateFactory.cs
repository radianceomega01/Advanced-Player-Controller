
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class StateFactory
{
    static PlayerState existingState;
    static List<PlayerState> playerStateList = new();
    public static PlayerState GetPlayerState(Type stateType, Player player)
    {
        //existingState = playerStateList.Where(a => a.GetType() == stateType);
        foreach (PlayerState state in playerStateList)
        {
            if (state.GetType() == stateType)
            { 
                existingState = state;
                break;
            }
        }
        if (existingState != null)
        {
            return existingState;
        }
        else
        {
            PlayerState newState = (PlayerState)Activator.CreateInstance(stateType, args:player);
            playerStateList.Add(newState);
            return newState;
        }
    }

}
