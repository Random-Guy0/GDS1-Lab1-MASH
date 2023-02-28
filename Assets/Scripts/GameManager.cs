using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        Vector2 maxPoint = mainCamera.ViewportToWorldPoint(new Vector2(0.9f, 0.9f));
        Vector2 minPoint = mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.1f));

        List <Vector2> potentialPoints = new List<Vector2>();

        Vector2 currentX = minPoint + new Vector2(placementDistance, 0.0f);
        while (currentX.x < maxPoint.x)
        {
            potentialPoints.Add(currentX);
            currentX.x += placementDistance;
        }
        
        Vector2 currentY = minPoint + new Vector2(0.0f, placementDistance);
        while (currentY.y < maxPoint.y)
        {
            potentialPoints.Add(currentY);
            currentY.y += placementDistance;
        }
        
        Vector2 currentBoth = minPoint + new Vector2(placementDistance, placementDistance);
        while (currentBoth.x < maxPoint.x && currentBoth.y < maxPoint.y)
        {
            potentialPoints.Add(currentBoth);
            currentBoth.x += placementDistance;
            currentBoth.y += placementDistance;
        }
        
        return potentialPoints;
    }
}
