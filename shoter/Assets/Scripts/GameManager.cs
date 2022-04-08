using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PhotonView pv;
    public static GameManager Instance;
    public bool pvp;

    [Header("Player Related")]
    public GameObject playerHandler;
    public GameObject playerSpawn;
    public Transform[] playerSpawns;
    public List<GameObject> players;
    public float respawns;

    [Header("MisteryBox")]
    public GameObject misteryBox;
    public Transform misteryBoxSpawn;

    [HideInInspector] public bool gameStarted;
    bool gameOver;
    float gameOverTimer;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        Instance = this;
    }
    private void Start()
    {
        gameOverTimer = 5;
        PhotonNetwork.AutomaticallySyncScene = true;
        Transform playerSpawn = playerSpawns[Random.Range(0, playerSpawns.Length)];
        PhotonNetwork.Instantiate(this.playerHandler.name, playerSpawn.position, playerSpawn.rotation);
        HUDManager.instance.respawnsCounter.text = respawns.ToString();
        SpawnMisteryBox();

        if (pvp)
        {
            gameStarted = true;
        }
    }

    private void Update()
    {
        for (var i = players.Count - 1; i > -1; i--)
        {
            if (players[i] == null)
                players.RemoveAt(i);
        }

        if(!pvp && players.Count == 0 && respawns == 0 && gameStarted)
        {
            pv.RPC("RPC_EndGame", RpcTarget.All);
        }

        if (gameOver)
        {
            HUDManager.instance.GameOver();
        }
    }
    public void CheckEnd()
    {
        if (pvp && respawns == 0)
        {
            pv.RPC("RPC_EndGame", RpcTarget.All);
        }
    }

    public void TakeRespawn()
    {
        if (!pvp)
        {
            pv.RPC("RPC_TakeRespawn", RpcTarget.All);
        }
        else
        {
            respawns--;
            HUDManager.instance.respawnsCounter.text = respawns.ToString();
        }
    }
    [PunRPC]
    void RPC_TakeRespawn()
    {
        print("taking respawn");
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
        if (!pvp)
        {
            pv.RPC("RPC_SpawnMisteryBox", RpcTarget.All);
        }
        
    }
    [PunRPC]
    void RPC_SpawnMisteryBox()
    {
        Instantiate(misteryBox, misteryBoxSpawn.position, misteryBox.transform.rotation);
    }

    [PunRPC]
    void RPC_EndGame()
    {
        StartCoroutine(_EndGame());
        IEnumerator _EndGame()
        {
            HUDManager.instance.GameOver();
            gameOver = true;
            yield return new WaitForSecondsRealtime(gameOverTimer);
            SceneManager.LoadScene(0);
        }
    }
}
