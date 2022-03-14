using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviour
{
    [HideInInspector] public PhotonView pv;

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    bool dead;
    public bool isRobot;
    public GameObject explosion;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        currentHealth = maxHealth;
        if (!pv.IsMine)
        {
            return;
        }
    }

    public void GetHit(float _damage)
    {
        pv.RPC("RPC_GetHit", RpcTarget.All, _damage);
    }
    [PunRPC]
    void RPC_GetHit(float _damage)
    {
        if (dead)
            return;

        currentHealth -= _damage;

        if (currentHealth <= 0)
        {
            dead = true;
            if (isRobot == true)
            {
                PhotonNetwork.Instantiate(explosion.name, gameObject.transform.position, Quaternion.identity);
                PhotonNetwork.Destroy(gameObject);
            }

            PhotonNetwork.Destroy(gameObject);
        }
    }
}
