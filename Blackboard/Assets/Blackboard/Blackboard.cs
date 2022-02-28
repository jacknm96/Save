using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Blackboard
{
    static Transform playerPosition;
    static bool playerFound;

    public static void SetPlayerPosition(Transform position)
    {
        playerPosition = position;
        playerFound = true;
    }

    public static bool IsPlayerFound()
    {
        return playerFound;
    }

    public static Transform GetPlayerPosition()
    {
        return playerPosition;
    }
}
