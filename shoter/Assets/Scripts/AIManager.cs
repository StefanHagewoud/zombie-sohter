using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AIManager : MonoBehaviour
{
    PhotonView pv;
    public static AIManager instance;
    public Terrain terrain;
    public GameObject[] robotPrefabs;
    public GameObject summonObject;
    public List<GameObject> robots;
    public GameObject[] players;

    public Transform[] spawnPositions;

    //RandomSpawnPosition
    public static float terrainLeft, terrainRight, terrainTop, terrainBottom, terrainWidth, terrainLength, terrainHeight;
    public LayerMask groundLayer;
    Transform spawnPos;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        instance = this;

        terrainLeft = terrain.transform.position.x;
        terrainBottom = terrain.transform.position.z;
        terrainWidth = terrain.terrainData.size.x;
        terrainLength = terrain.terrainData.size.z;
        terrainHeight = terrain.terrainData.size.y;
        terrainRight = terrainLeft + terrainWidth;
        terrainTop = terrainBottom + terrainLength;

        UpdatePlayers();
        WaveStart();
    }

    public void WaveStart()
    {
        StartCoroutine(FirstWave());
        UpdatePlayers();
        IEnumerator FirstWave()
        {
            yield return new WaitForSecondsRealtime(4);
            SpawnRobot(10, 0);
        }
    }

    public void UpdatePlayers()
    {
        print("update");
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        players = gameObjects;

        foreach (GameObject robot in robots)
        {
            robot.GetComponent<AIBasics>().UpdateTarget();
        }
   
    }
    public void SpawnRobot(int amount, float addedHeight)
    {
        var i = 0;
        float terrainHeight = 0f;
        RaycastHit hit;
        float randomPositionX, randomPositionY, randomPositionZ;
        Vector3 randomPosition = Vector3.zero;

        do
        {
            i++;
            randomPositionX = Random.Range(terrainLeft, terrainRight);
            randomPositionZ = Random.Range(terrainBottom, terrainTop);

            if (Physics.Raycast(new Vector3(randomPositionX, 9999f, randomPositionZ), Vector3.down, out hit, Mathf.Infinity, groundLayer))
            {
                terrainHeight = hit.point.y;
            }
            randomPositionY = terrainHeight + addedHeight;
            randomPosition = new Vector3(randomPositionX, randomPositionY, randomPositionZ);
            GameObject currentRobotPrefab = robotPrefabs[Random.Range(0, robotPrefabs.Length)];
            PhotonNetwork.Instantiate(summonObject.name, randomPosition, Quaternion.identity);
            summonObject.GetComponent<RobotSummon>().robotToSpawn = currentRobotPrefab;
        }
        while (i < amount);
    }
}
