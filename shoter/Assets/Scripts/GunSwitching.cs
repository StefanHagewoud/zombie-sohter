using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public Transform gunContainer;
    public bool switchWeapon;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    public void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Drop();
                    weapon.SetParent(null);

                }
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }

    }

    public void Drop()
    {
        //equipped = false;
        gunContainer.GetComponentInChildren<GunScript>().pistol = false;
        gunContainer.GetComponentInChildren<GunScript>().enabled = false;
        gunContainer.GetComponentInChildren<Rigidbody>().isKinematic = false;
        gunContainer.GetComponentInChildren<BoxCollider>().enabled = true;


        //transform.SetParent(null);
        //rb.isKinematic = false;
        //coll.isTrigger = false;

        // transform.SetParent(null);
        //disable gunscript
        // gunScript.enabled = false;

        // rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //rb.AddForce(fpscam.forward * dropForwardForce, ForceMode.Impulse);
        //rb.AddForce(fpscam.up * dropUpwardForce, ForceMode.Impulse);

        //float random = Random.Range(-1f, 1f);
        //rb.AddTorque(new Vector3(random, random, random) * 10);
    }

    void SelectWeapon()
    {
   
    }
}
