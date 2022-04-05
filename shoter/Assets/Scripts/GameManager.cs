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

    public GameObject misteryBox;
    public Transform misteryBoxSpawn;

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
        HUDManager.instance.respawnsCounter.text = respawns.ToString();
        SpawnMisteryBox();
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
        HUDManager.instance.respawnsCounter.text = respawns.ToString();
    }
    public void HealAllPlayers()
    {
        pv.RPC("RPC_HealAllPlayers", RpcTarget.All);
    }
    [PunRPC]
    void RPC_HealAllPlayers()
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<Health>().currentHealth = 100;
        }
    }
    public void SpawnMisteryBox()
    {
        pv.RPC("RPC_SpawnMisteryBox", RpcTarget.All);
    }
    [PunRPC]
    void RPC_SpawnMisteryBox()
    {
        Instantiate(misteryBox, misteryBoxSpawn.position, misteryBox.transform.rotation);
    }
    public void PlayerWin()
    {

    }
    public void RobotWin()
    {

    }
}
