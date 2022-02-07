using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public TMP_InputField CreateGameInput;


    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(Random.Range(1, 9999).ToString("0000"));
    }
    public void JoinRandomLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }
}
