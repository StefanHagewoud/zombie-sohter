using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBasics : MonoBehaviour
{
    NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    public void UpdateTarget()
    {

    }
}
