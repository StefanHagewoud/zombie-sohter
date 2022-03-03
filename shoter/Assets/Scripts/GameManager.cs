using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public PhotonView pv;

    public GameManager Instance;

    public GameObject[] Players;
    public float revives;
    private void Awake()
    {
        Instance = this;
        pv = GetComponent<PhotonView>();
    }
    private void Start()
    {
        PlayerWin();
    }
    public void UpdatePlayerlist()
    {
        GameObject.FindGameObjectsWithTag("Players");
    }
    public void PlayerWin()
    {

    }
    public void RobotWin()
    {

    }
}
