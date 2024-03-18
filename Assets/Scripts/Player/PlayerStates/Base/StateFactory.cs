
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class StateFactory
{
    static List<PlayerState> playerStateList = new();
    public static PlayerState GetPlayerState(Type stateType, Player player)
    {
        for (int i = 0; i < playerStateList.Count; i++)
        {
            if (playerStateList[i].GetType() == stateType)
            {
                return (playerStateList[i]);
            }
        }

        PlayerState newState = (PlayerState)Activator.CreateInstance(stateType, args: player);
        playerStateList.Add(newState);
        return newState;
    }

}
