using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    PhotonView pv;

    public static GameManager Instance;

    public GameObject playerPrefab;
    public Transform[] playerSpawns;

    public GameObject[] players;
    public float revives;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        Instance = this;
    }
    private void Start()
    {
        Transform playerSpawn = playerSpawns[Random.Range(0, playerSpawns.Length)];
        PhotonNetwork.Instantiate(this.playerPrefab.name, playerSpawn.position, playerSpawn.rotation);
        UpdatePlayerlist();
    }
    public void UpdatePlayerlist()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        AIManager.instance.UpdatePlayers();
    }
    public void PlayerWin()
    {

    }
    public void RobotWin()
    {

    }
}
