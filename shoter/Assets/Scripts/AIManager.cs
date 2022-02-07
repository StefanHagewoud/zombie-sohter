using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager instance;

    public GameObject[] robots;
    public GameObject[] players;

    private void Awake()
    {
        instance = this;
        UpdatePlayers();
    }

    public void SpawnRobot()
    {
        
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
