using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int soldiersOnField;
    
    [SerializeField] private TMP_Text soldiersInHelicopterText;
    [SerializeField] private TMP_Text soldiersRescuedText;

    [SerializeField] private Helicopter helicopter;

    [SerializeField] private GameObject winScreen;
    
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
        soldiersOnField -= soldiersInHelicopter;
        soldiersInHelicopter = 0;
        UpdateSoldiersInHelicopterText();
        UpdateSoldiersRescuedText();

        if (soldiersOnField == 0)
        {
            Win();
        }
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
        helicopter.EndGame();
    }

    public void Win()
    {
        winScreen.SetActive(true);
        helicopter.EndGame();
    }

    private void UpdateSoldiersInHelicopterText()
    {
        soldiersInHelicopterText.SetText("Soldiers In Helicopter: " + soldiersInHelicopter);
    }

    private void UpdateSoldiersRescuedText()
    {
        soldiersRescuedText.SetText("Soldiers Rescued: " + soldiersRescued);
    }

    public void ResetGame()
    {
        
    }
}
