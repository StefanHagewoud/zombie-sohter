using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerHandler : MonoBehaviour
{
    PhotonView pv;
    public GameObject playerPrefab;
    [HideInInspector]public GameObject playerSpawn;
    public GameObject myPlayer;
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
            myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, playerSpawn.transform.position, Quaternion.identity);
            myPlayer.gameObject.GetComponent<Health>().playerHandler = this.gameObject;
            firstSpawn = false;
        }
    }

    public void ReSpawnPlayer()
    {
        if (pv.IsMine)
        {
            StartCoroutine(respawn());
            IEnumerator respawn()
            {
                yield return new WaitForSecondsRealtime(3);
                if (GameManager.Instance.respawns > 0)
                {
                    myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, playerSpawn.transform.position, Quaternion.identity);
                    myPlayer.gameObject.GetComponent<Health>().playerHandler = this.gameObject;
                    GameManager.Instance.TakeRespawn();
                }
                else
                {
                    if (GameManager.Instance.respawns <= 0)
                    {
                        Cursor.lockState = CursorLockMode.None;
                    }
                }
            }
        }
    }
}
