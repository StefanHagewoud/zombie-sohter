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
        ExplosionDamage(hitBoxAOE.transform.position, 1.5f);
    }

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.tag == "Player")
            {
                hitCollider.GetComponent<Health>().GetHit(damage);
            }
        }
    }

    IEnumerator HitBox()
    {
        hitBoxAOE.GetComponent<Collider>().enabled = true;
        yield return new WaitForSecondsRealtime(0.5f);
        hitBoxAOE.GetComponent<Collider>().enabled = false;
        PhotonNetwork.Destroy(gameObject);
    }
}
