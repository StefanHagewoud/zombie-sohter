using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviour
{
    PhotonView pv;
    public static LobbyManager Instance;
    public GameObject readyButton;

    public GameObject lobbySettingsPanel;
    public GameObject lobbyInfoPanel;

    public GameObject lobbySettingsPanelPvp;
    public GameObject lobbyInfoPanelPvp;

    public GameObject playerLobbyItem;

    public Transform playersPanel;
    public Transform playersPanelPvP;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();

        Instance = this;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {

    }

    public void SetupRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                lobbySettingsPanel.SetActive(true);
                lobbyInfoPanel.SetActive(false);
            }
            else
            {
                lobbySettingsPanel.SetActive(false);
                lobbyInfoPanel.SetActive(true);
            }
        }
        pv.RPC("RPC_SpawnPlayerLobbyItem", RpcTarget.AllBuffered, PhotonNetwork.NickName, PhotonNetwork.LocalPlayer);
        return;
    }

    public void SetupPvP()
    {
        if (PhotonNetwork.InRoom)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                lobbySettingsPanelPvp.SetActive(true);
                lobbyInfoPanelPvp.SetActive(false);
            }
            else
            {
                lobbySettingsPanelPvp.SetActive(false);
                lobbyInfoPanelPvp.SetActive(true);
            }
        }

        pv.RPC("RPC_SpawnPlayerLobbyItemPvP", RpcTarget.AllBuffered, PhotonNetwork.NickName, PhotonNetwork.LocalPlayer);
        return;
    }

    [PunRPC]
    void RPC_SpawnPlayerLobbyItem(string _playerName, Player _player)
    {
        PlayerLobbyItem _playerLobbyItem = Instantiate(playerLobbyItem, playersPanel).GetComponent<PlayerLobbyItem>();
        _playerLobbyItem.PlayerSetup(_playerName, _player);
        Debug.Log(_playerName);
    }
    [PunRPC]
    void RPC_SpawnPlayerLobbyItemPvP(string _playerName, Player _player)
    {
        PlayerLobbyItem _playerLobbyItem = Instantiate(playerLobbyItem, playersPanelPvP).GetComponent<PlayerLobbyItem>();
        _playerLobbyItem.PlayerSetup(_playerName, _player);
        Debug.Log(_playerName);
    }

    public void StartGame()
    {
        if (MainMenu.Instance.pvp)
        {
            PhotonNetwork.LoadLevel(2);
        }
        else
        {
            PhotonNetwork.LoadLevel(1);
        }
    }
}
