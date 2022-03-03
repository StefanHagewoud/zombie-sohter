using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BigRobot : AIBasics
{
    PhotonView pv;

    public GameObject GuidedRocket;

    public GameObject rightRocketSpawn;
    public GameObject leftRocketSpawn;

    public float rocketReloadTime;
    bool reloading;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    public override void Update()
    {
        base.Update();


        if (!reloading)
        {
            nav.speed = moveSpeed;
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                nav.speed = 0;
                
                FireMainWeapon();
            }

        }

        if(nav.remainingDistance <= nav.stoppingDistance)
        {
            anim.SetFloat("Blend", 1f, 0.1f, Time.deltaTime);
            gameObject.transform.LookAt(targetDestination);
        }
        else
        {
            anim.SetFloat("Blend", 0f, 0.1f, Time.deltaTime);
        }
    }

    public void FireMainWeapon()
    {
        StartCoroutine(Shooting());
        IEnumerator Shooting()
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Shooting"), 1);
            yield return new WaitForSecondsRealtime(0.58f);
            anim.SetLayerWeight(anim.GetLayerIndex("Shooting"), 0);
            nav.speed = moveSpeed;
            StartCoroutine(Reloading());
        }
        IEnumerator Reloading()
        {
            reloading = true;
            yield return new WaitForSecondsRealtime(rocketReloadTime);
            reloading = false;
        }
    }
    public void InstantiateRocketRight()
    {
        PhotonNetwork.Instantiate(this.GuidedRocket.name, rightRocketSpawn.transform.position, rightRocketSpawn.transform.rotation);
    }
    public void InstantiateRocketLeft()
    {
        PhotonNetwork.Instantiate(this.GuidedRocket.name, leftRocketSpawn.transform.position, rightRocketSpawn.transform.rotation);
    }
}
