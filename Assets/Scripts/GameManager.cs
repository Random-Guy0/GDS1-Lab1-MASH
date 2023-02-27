using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int soldiersInHelicopter;
    private int soldiersRescued;

    private void Start()
    {
        soldiersInHelicopter = 0;
        soldiersRescued = 0;
    }

    public void PickupSoldier()
    {
        soldiersInHelicopter++;
    }

    public int GetSoldiersInHelicopter()
    {
        return soldiersInHelicopter;
    }

    public void DropOffSoldiers()
    {
        soldiersRescued += soldiersInHelicopter;
        soldiersInHelicopter = 0;
    }
}
