using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class PlayerLobbyItem : MonoBehaviour
{
    [SerializeField] TMP_Text playerName;
    public Player player;

    public void PlayerSetup(string name, Player _player)
    {
        playerName.text = name;
        player = _player;
    }
}
