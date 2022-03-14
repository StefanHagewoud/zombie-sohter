using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RocketExplosion : MonoBehaviour
{
    PhotonView pv;
    [HideInInspector] public float damage;

    public GameObject hitBoxAOE;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        StartCoroutine(HitBox());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<Health>().GetHit(damage);
        }
    }

    IEnumerator HitBox()
    {
        hitBoxAOE.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        hitBoxAOE.SetActive(false);
        PhotonNetwork.Destroy(gameObject);
    }
}
