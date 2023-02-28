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
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private GameObject soldierPrefab;
    
    private int soldiersInHelicopter;
    private int soldiersRescued;

    private GameObject[] soldiers;
    private Vector2[] soldierPositions;

    private void Start()
    {
        soldiersInHelicopter = 0;
        soldiersRescued = 0;

        soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        soldierPositions = new Vector2[soldiers.Length];
        for (int i = 0; i < soldiers.Length; i++)
        {
            soldierPositions[i] = soldiers[i].transform.position;
        }

        //soldiersOnField = soldiers.Length;
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
        gameOverScreen.SetActive(true);
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
        soldiersRescued = 0;
        soldiersInHelicopter = 0;
        UpdateSoldiersInHelicopterText();
        UpdateSoldiersRescuedText();

        soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        foreach (GameObject soldier in soldiers)
        {
            Destroy(soldier);
        }
        
        for (int i = 0; i < soldierPositions.Length; i++)
        {
            GameObject.Instantiate(soldierPrefab, soldierPositions[i], Quaternion.identity);
        }
        
        soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        soldiersOnField = soldiers.Length;
        
        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        helicopter.StartGame();
    }
}
