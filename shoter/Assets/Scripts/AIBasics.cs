using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBasics : MonoBehaviour
{
    public NavMeshAgent nav;
    AIManager _AIManager;

    public Animator anim;
    float moveSpeed;
    //Targeting
    public GameObject[] players;
    [HideInInspector]public Transform targetDestination;
    void Start()
    {
        _AIManager = FindObjectOfType<AIManager>();
        players = _AIManager.players;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        moveSpeed = nav.speed;
        //GetClosestPlayer();
        UpdateTarget();
    }


    public void UpdateTarget()
    {
        GetClosestPlayer();
        nav.destination = targetDestination.transform.position;
    }

    public void GetClosestPlayer()
    {
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
        targetDestination = currentClosest.transform;
    }

}
