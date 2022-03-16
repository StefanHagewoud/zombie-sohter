using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    PhotonView pv;

    public static GameManager Instance;

    public GameObject playerHandler;
    public GameObject playerSpawn;

    public Transform[] playerSpawns;

    public GameObject[] players;

    [Header("PlayerSpawns")]
    public float respawns;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        Instance = this;
    }
    private void Start()
    {
        Transform playerSpawn = playerSpawns[Random.Range(0, playerSpawns.Length)];
        PhotonNetwork.Instantiate(this.playerHandler.name, playerSpawn.position, playerSpawn.rotation);
    }
    public void UpdatePlayerlist()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        AIManager.instance.UpdatePlayers();
    }
    [PunRPC]
    void RPC_UpdatePlayerList()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void PlayerWin()
    {

    }
    public void RobotWin()
    {

    }
}
