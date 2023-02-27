using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text soldiersInHelicopterText;
    [SerializeField] private TMP_Text soldiersRescuedText;
    
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
        UpdateSoldiersInHelicopterText();
    }

    public int GetSoldiersInHelicopter()
    {
        return soldiersInHelicopter;
    }

    public void DropOffSoldiers()
    {
        soldiersRescued += soldiersInHelicopter;
        soldiersInHelicopter = 0;
        UpdateSoldiersInHelicopterText();
        UpdateSoldiersRescuedText();
    }

    private void UpdateSoldiersInHelicopterText()
    {
        soldiersInHelicopterText.SetText("Soldiers In Helicopter: " + soldiersInHelicopter);
    }

    private void UpdateSoldiersRescuedText()
    {
        soldiersRescuedText.SetText("Soldiers Rescued: " + soldiersRescued);
    }
}
