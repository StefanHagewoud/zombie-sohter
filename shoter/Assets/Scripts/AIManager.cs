using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager instance;

    public GameObject[] robotPrefabs;
    public GameObject currentRobotPrefab;
    public GameObject[] robots;
    public GameObject[] players;

    public Transform[] spawnPositions;

    private void Awake()
    {
        instance = this;
        UpdatePlayers();
        SpawnRobot();
        SpawnRobot();
        SpawnRobot();
        SpawnRobot();
        SpawnRobot();
    }

    public void SpawnRobot()
    {
        Transform spawnPos = spawnPositions[Random.Range(0, spawnPositions.Length)];
        Instantiate(currentRobotPrefab = robotPrefabs[Random.Range(0, robotPrefabs.Length)], spawnPos.position, Quaternion.identity);
    }

    public void UpdatePlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            
        }

        foreach(GameObject robot in robots)
        {
            robot.GetComponent<AIBasics>().UpdateTarget();
        }
    }
}
