using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Vector2Int minMaxSoldiers;
    [SerializeField] private Vector2Int minMaxTrees;

    private int soldiersOnField;

    [SerializeField] private TMP_Text soldiersInHelicopterText;
    [SerializeField] private TMP_Text soldiersRescuedText;

    [SerializeField] private Helicopter helicopter;

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private GameObject treePrefab;

    [SerializeField] private AudioSource soldierPickupSound;

    [SerializeField] private float placementDistance;
    
    private int soldiersInHelicopter;
    private int soldiersRescued;

    private GameObject[] soldiers;
    private GameObject[] trees;

    private void Start()
    {
        ResetGame();
    }

    public void PickupSoldier()
    {
        soldiersInHelicopter++;
        UpdateSoldiersInHelicopterText();
        soldierPickupSound.Play();
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

    public void ResetKey(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ResetGame();
        }
    }

    public void ResetGame()
    {
        int numSoldiers = Random.Range(minMaxSoldiers.x, minMaxSoldiers.y + 1);
        int numTrees = Random.Range(minMaxTrees.x, minMaxTrees.y + 1);
        
        soldiersOnField = numSoldiers;
        soldiersRescued = 0;
        soldiersInHelicopter = 0;
        UpdateSoldiersInHelicopterText();
        UpdateSoldiersRescuedText();

        if (soldiers != null)
        {
            foreach (GameObject soldier in soldiers)
            {
                Destroy(soldier);
            }
        }

        if (trees != null)
        {
            foreach (GameObject tree in trees)
            {
                Destroy(tree);
            }
        }

        List<Vector2> potentialPoints = GeneratePotentialPoints();
        
        soldiers = new GameObject[numSoldiers];
        for (int i = 0; i < numSoldiers; i++)
        {
            int chosenPointIndex = Random.Range(0, potentialPoints.Count);
            soldiers[i] = GameObject.Instantiate(soldierPrefab ,potentialPoints[chosenPointIndex], Quaternion.identity);
            potentialPoints.RemoveAt(chosenPointIndex);
        }

        trees = new GameObject[numTrees];
        for (int i = 0; i < numTrees; i++)
        {
            int chosenPointIndex = Random.Range(0, potentialPoints.Count);
            trees[i] = GameObject.Instantiate(treePrefab ,potentialPoints[chosenPointIndex], Quaternion.identity);
            potentialPoints.RemoveAt(chosenPointIndex);
        }

        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        helicopter.StartGame();
    }

    private List<Vector2> GeneratePotentialPoints()
    {
        Camera mainCamera = Camera.main;
        Vector2 maxPoint = mainCamera.ViewportToWorldPoint(new Vector2(0.95f, 0.95f));
        Vector2 minPoint = mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.1f));

        List <Vector2> potentialPoints = new List<Vector2>();

        Vector2 current = minPoint;
        while (current.x < maxPoint.x)
        {
            potentialPoints.Add(current);

            current.y += placementDistance;
            while (current.y <= maxPoint.y)
            {
                potentialPoints.Add(current);
                current.y += placementDistance;
            }

            current.x += placementDistance;
            current.y = minPoint.y;
        }
        
        return potentialPoints;
    }
}
