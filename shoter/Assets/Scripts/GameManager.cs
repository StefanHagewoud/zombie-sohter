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


    public List<GameObject> players;

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

    private void Update()
    {
        for (var i = players.Count - 1; i > -1; i--)
        {
            if (players[i] == null)
                players.RemoveAt(i);
        }
    }
    public void TakeRespawn()
    {
        pv.RPC("RPC_TakeRespawn", RpcTarget.All);
    }
    [PunRPC]
    void RPC_TakeRespawn()
    {
        respawns--;
    }
    public void PlayerWin()
    {

    }
    public void RobotWin()
    {

    }
}
