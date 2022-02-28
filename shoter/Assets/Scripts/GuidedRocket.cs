using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedRocket : MonoBehaviour
{
    public float speed;


    [HideInInspector]public GameObject[] players;
    public GameObject target;


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
        
    }
}
