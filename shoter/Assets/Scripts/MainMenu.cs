using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public TMP_InputField CreateGameInput;
    public static MainMenu Instance;

    [SerializeField] TMP_Text usernameInput;
    private void Awake()
    {
        Instance = this;
    }

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
        SetUsername();
    }
    public void JoinRandomLobby()
    {
        PhotonNetwork.JoinRandomRoom();
        SetUsername();
    }
    public void SetUsername()
    {
        PhotonNetwork.NickName = usernameInput.text;
        if (PhotonNetwork.NickName == "")
        {
            PhotonNetwork.NickName = "Player" + Random.Range(0, 99).ToString("00");
        }
    }
    public override void OnJoinedRoom()
    {
        MenuManager.Instance.SwitchMenu("Lobby");
        LobbyManager.Instance.SetupRoom();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
