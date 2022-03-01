using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager instance;

    public GameObject[] robotPrefabs;
    public List<GameObject> robots;
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


    public void CurrentWave()
    {

    }
    public void SpawnRobot()
    {
        Transform spawnPos = spawnPositions[Random.Range(0, spawnPositions.Length)];
        GameObject currentRobotPrefab = robotPrefabs[Random.Range(0, robotPrefabs.Length)];
        Instantiate(currentRobotPrefab, spawnPos.position, Quaternion.identity);
        robots.Add(currentRobotPrefab);
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
