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
    [HideInInspector]public AudioSource audioS;
    public float damage;
    
    [HideInInspector]public float moveSpeed;
    //Targeting
    public List<GameObject> players;

    public Transform targetDestination;
    public Transform lastTargetPosition;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();

        if (!pv.IsMine)
            return;
    }
    void Start()
    {
        _AIManager = FindObjectOfType<AIManager>();
        players = GameManager.Instance.players;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();
        moveSpeed = nav.speed;

        AIManager.instance.robots.Add(gameObject);

        
        nav.destination = targetDestination.position;

        if (pv.IsMine)
        {
            UpdateTarget();
            InvokeRepeating("UpdateTarget", 0, 5);
        }
    }

    public void UpdateTarget()
    {
        players = GameManager.Instance.players;
        if (players == null)
            return;
        nav.ResetPath();
        GetClosestPlayer();
    }

    public virtual void Update()
    {
        if (pv.IsMine)
        {
            if (targetDestination == null)
            {
                UpdateTarget();
            }
            else
            {
                nav.SetDestination(lastTargetPosition.localPosition);
            }

            if (!nav.isOnNavMesh)
            {
                gameObject.GetComponent<Health>().GetHit(9999);
            }
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
        lastTargetPosition = targetDestination;
    }
}
