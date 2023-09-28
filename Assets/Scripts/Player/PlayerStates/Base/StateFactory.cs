
using System;
using System.Collections.Generic;
using System.Linq;

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
                existingState = state;
        }
        if (existingState != null)
            return existingState;
        else
        {
            PlayerState newState = (PlayerState)Activator.CreateInstance(stateType, args:player);
            playerStateList.Add(newState);
            return newState;
        }
    }

    /*public static PlayerState GetIdleState(Player player)
    {
        if (idleState == null)
            idleState = new IdleState(player);
        return idleState;
    }*/

}
