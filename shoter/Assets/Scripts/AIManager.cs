using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    PhotonView pv;

    [Header("ESSENTIALS")]
    public static AIManager instance;
    public Terrain terrain;
    public GameObject[] robotPrefabs;
    public GameObject summonObject;
    public List<GameObject> robots;
    public float robotsAmount;
    public GameObject[] players;
    public NavMeshHit navmesh;

    [Header("WAVES")]
    float waveNumber;
    bool waveStarted;
    public float robotWaveStartAmount;
    public float robotWaveAmountMultiplier;
    float robotWaveAmount;

    [Header("SPAWNS")]
    public static float terrainLeft, terrainRight, terrainTop, terrainBottom, terrainWidth, terrainLength, terrainHeight;
    public LayerMask groundLayer;
    Transform spawnPos;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        instance = this;

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

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
    private void Update()
    {
        for (var i = robots.Count - 1; i > -1; i--)
        {
            if (robots[i] == null)
                robots.RemoveAt(i);
            UpdateHUD();
        }

        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                SpawnRobot(10, 0);  
            }
        }
        if(waveStarted == true)
        {
            if(robotsAmount == 0)
            {
                GameManager.Instance.HealAllPlayers();

                waveStarted = false;
                NextWave();
            }
        }
    }
    public void WaveStart()
    {
        waveStarted = true;
        StartCoroutine(FirstWave());
        UpdatePlayers();
        IEnumerator FirstWave()
        {
            yield return new WaitForSecondsRealtime(4);
            SpawnRobot(10, 0);
        }
    }
    public void NextWave()
    {
        waveNumber++;
        waveStarted = true;
        int _robotWaveAmount = ((int)robotWaveAmount);
        SpawnRobot(_robotWaveAmount, 0);
        robotWaveAmount += players.Length * robotWaveAmountMultiplier;
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
        waveStarted = true;

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
            robotsAmount += 1;
            summonObject.GetComponent<RobotSummon>().robotToSpawn = currentRobotPrefab;
        }
        while (i < amount);
    }
    public void UpdateHUD()
    {
        pv.RPC("RPC_UpdateHUD", RpcTarget.All);
    }

    [PunRPC]
    void RPC_UpdateHUD()
    {
        HUDManager.instance.waveCounter.text = waveNumber.ToString();
        HUDManager.instance.robotCounter.text = robots.Count.ToString();
    }
}
