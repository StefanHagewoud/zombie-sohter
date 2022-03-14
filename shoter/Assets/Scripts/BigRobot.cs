using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BigRobot : AIBasics
{
    public GameObject GuidedRocket;

    public GameObject rightRocketSpawn;
    public GameObject leftRocketSpawn;

    public float rocketReloadTime;
    bool reloading;
    float damping = 1.5f;

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
            var lookPos = targetDestination.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
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
        GuidedRocket.GetComponent<GuidedRocket>().damage = damage;
    }
    public void InstantiateRocketLeft()
    {
        PhotonNetwork.Instantiate(this.GuidedRocket.name, leftRocketSpawn.transform.position, rightRocketSpawn.transform.rotation);
        GuidedRocket.GetComponent<GuidedRocket>().damage = damage;
    }
}
