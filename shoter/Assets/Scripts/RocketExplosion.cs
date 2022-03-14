using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RocketExplosion : MonoBehaviour
{
    PhotonView pv;
    ParticleSystem ps;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        ps = GetComponentInChildren<ParticleSystem>();
    }
}
