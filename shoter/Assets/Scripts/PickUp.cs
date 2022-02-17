using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GunScript gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpscam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;
    public static int gunCounter;

    public bool equipped;
    public static bool slotfull;

    public Vector3 pickUpOffset;

    private void Start()
    {
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotfull = true;
        }
    }

    private void Update()
    {
        //check if player is in range and E is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E))
        {
            if(gunContainer.childCount < 2)
            {
                Pickup();
            }
        }
        //drop if pressed Q
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }    
    }

    private void Pickup()
    {
        equipped = true;
        slotfull = true;
        
    
        //make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        //transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        //transform.localScale = Vector3.one;
        gunContainer.GetComponent<GunSwitching>().selectedWeapon = gunContainer.childCount -1;

        //make rigidbody kinematic and boxcollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //enable script
        gunScript.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotfull = false;

        rb.isKinematic = false;
        coll.isTrigger = false;

        transform.SetParent(null);
        //disable gunscript
        gunScript.enabled = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(fpscam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpscam.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
    }
}
