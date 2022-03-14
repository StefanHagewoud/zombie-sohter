using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class GuidedRocket : MonoBehaviour
{
    PhotonView pv;
    public float speed;
    public GameObject explosion;
    [HideInInspector] public float damage;
    [HideInInspector]public GameObject[] players;
    public GameObject target;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        GameObject currentClosest = null;
        float currentClosestDistance = 0;
        foreach (GameObject player in players)
        {
            if (currentClosest == null)
            {
                currentClosest = player;
                currentClosestDistance = Vector3.Distance(transform.position, currentClosest.transform.position);
            }
            else
            {
                float newDistance = Vector3.Distance(transform.position, currentClosest.transform.position);
                if (newDistance < currentClosestDistance)
                {
                    currentClosest = player;
                    currentClosestDistance = newDistance;
                }
            }
        }
        target = currentClosest;
    }

    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.5f * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        PhotonNetwork.Instantiate(this.explosion.name, transform.position, Quaternion.identity);
        explosion.GetComponent<RocketExplosion>().damage = damage;
        PhotonNetwork.Destroy(gameObject);
    }
}
