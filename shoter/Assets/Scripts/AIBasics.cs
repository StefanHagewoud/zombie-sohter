using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class AIBasics : MonoBehaviour
{
    PhotonView pv;

    [HideInInspector]public NavMeshAgent nav;
    [HideInInspector]public Animator anim;
    [HideInInspector] public Rigidbody rb;
    AIManager _AIManager;

    public float damage;
    
    [HideInInspector]public float moveSpeed;
    //Targeting
    public GameObject[] players;
    public Transform targetDestination;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    void Start()
    {
        _AIManager = FindObjectOfType<AIManager>();
        players = GameManager.Instance.players;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        moveSpeed = nav.speed;
        UpdateTarget();
        InvokeRepeating("UpdateTarget", 0, 5);
    }

    public void UpdateTarget()
    {
        players = GameManager.Instance.players;
        if (players == null)
            return;
        GetClosestPlayer();
    }

    public virtual void Update()
    {
        if (targetDestination == null)
        {
            UpdateTarget();
        }
        else
        {
            nav.destination = targetDestination.transform.position;
        }
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
