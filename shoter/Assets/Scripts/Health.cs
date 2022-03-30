using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviour
{
    [HideInInspector] public PhotonView pv;

    public GameObject playerHandler;
    [SerializeField] private float maxHealth;
    public float currentHealth;

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

        if (!isRobot)
        {
            pv.RPC("RPC_AddPlayerToList", RpcTarget.All);
        }
    }
    private void Start()
    {
        if(!isRobot && pv.IsMine)
        {
            HUDManager.instance.currentHealthText.text = currentHealth.ToString();
            HUDManager.instance.maxHealthText.text = maxHealth.ToString();
        }
    }

    [PunRPC]
    void RPC_AddPlayerToList()
    {
        GameManager.Instance.players.Add(gameObject);
    }

    public void GetHit(float _damage)
    {
        pv.RPC("RPC_GetHit", RpcTarget.All, _damage);
        if(!isRobot && pv.IsMine)
        {
            HUDManager.instance.currentHealthText.text = currentHealth.ToString();
        }
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
                AIManager.instance.robotsAmount--;
                PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                playerHandler.GetComponent<PlayerHandler>().ReSpawnPlayer();
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
