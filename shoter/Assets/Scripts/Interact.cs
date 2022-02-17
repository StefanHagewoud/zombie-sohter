using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public float interactOffset;
    public float interactRadius;
    private GunSwitching gunSwitching;

    private void Awake()
    {
        gunSwitching = GetComponentInChildren<GunSwitching>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 offset = transform.position + (transform.forward * interactOffset);
        Gizmos.DrawWireSphere(offset, interactRadius);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Vector3 offset = transform.position + (transform.forward * interactOffset);
            Collider[] interacts = Physics.OverlapSphere(offset, interactRadius);
            Collider closestCol = null;
            float closestFloat = 0;
            foreach(Collider col in interacts)
            {
                float dist = Vector3.Distance(offset, col.transform.position);
                if(closestCol == null)
                {
                    closestCol = col;
                    closestFloat = dist;
                }
                else if(closestFloat > dist)
                {
                    closestCol = col;
                    closestFloat = dist;
                }
            }
            PickUp(closestCol);
        }
        if (Input.GetButtonDown("Drop"))
        {
            Drop();
        }

    }
    void PickUp(Collider col)
    {
        if (col.GetComponent<PickUp>())
        {
            //col.GetComponent<PickUp>().Pickup();
        }
    }

    void Drop()
    {

    }
}
