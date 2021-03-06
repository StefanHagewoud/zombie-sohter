using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using TMPro;

public class PickUp : MonoBehaviour
{
    public Transform player, gunContainer;
    public TwoBoneIKConstraint rightHandIK;

    public Camera fpsCam;
    public RaycastHit hit;
    public float range;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;


    public void Start()
    {
        fpsCam = Camera.main;
    }
    private void Update()
    {
        //check if player is in range and E is pressed
        //Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && gunContainer.childCount <= 2 && Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.CompareTag("PickUpAble") && gunContainer.childCount < 2)
                {
                    PickUpItem();
                }

                if (hit.transform.CompareTag("LootBox") && gunContainer.childCount <= 2)
                {
                    GameObject.FindGameObjectWithTag("LootBox").GetComponent<LootBox>().ShowItem();
                }
            }
        }
    }

    public void PickUpItem()
    {
        //equipped = true;
        hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
        hit.transform.gameObject.GetComponent<GunScript>().enabled = true;
        hit.transform.gameObject.GetComponent<GunScript>().fpsCam = fpsCam;

        //make weapon a child of the camera and move it to default position
        hit.transform.SetParent(gunContainer);
        //transform.localPosition = Vector3.zero;
        hit.transform.localRotation = Quaternion.Euler(Vector3.zero);
        //transform.localScale = Vector3.one;
        gunContainer.GetComponent<GunSwitching>().selectedWeapon = gunContainer.childCount - 1;


        //make rigidbody kinematic and boxcollider a trigger
        //rb.isKinematic = true;
        //coll.isTrigger = true;

        //enable script
        //gunScript.enabled = true;
    }

}
