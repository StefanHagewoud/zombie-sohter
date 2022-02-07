using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;

    private void Awake()
    {
        instance = this;
    }
}
