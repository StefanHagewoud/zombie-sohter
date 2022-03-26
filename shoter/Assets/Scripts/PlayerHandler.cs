using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerHandler : MonoBehaviour
{
    PhotonView pv;
    public GameObject playerPrefab;
    [HideInInspector]public GameObject playerSpawn;

    bool firstSpawn = true;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
            PhotonNetwork.Destroy(gameObject);
    }

    private void Start()
    {
        if (pv.IsMine)
        {
            playerSpawn = GameManager.Instance.playerSpawn;

            //first spawn
            PhotonNetwork.Instantiate(playerPrefab.name, playerSpawn.transform.position, Quaternion.identity);
            playerPrefab.GetComponent<Health>().playerHandler = this.gameObject;
            firstSpawn = false;
        }
    }

    public void ReSpawnPlayer()
    {
        if (pv.IsMine)
        {
            if (GameManager.Instance.respawns > 0)
            {
                PhotonNetwork.Instantiate(playerPrefab.name, playerSpawn.transform.position, Quaternion.identity);
                playerPrefab.GetComponent<Health>().playerHandler = this.gameObject;
                GameManager.Instance.respawns--;
            }
            else
            {
                //spectate?
            }
        }
    }
}
