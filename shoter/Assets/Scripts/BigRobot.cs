using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRobot : AIBasics
{
    public GameObject GuidedRocket;

    public GameObject rightRocketSpawn;
    public GameObject leftRocketSpawn;

    public float rocketReloadTime;
    bool reloading;
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

        //if(rb.velocity <= )
        //{
        //    anim.SetFloat("Blend", 1f, 0.1f, Time.deltaTime);
        //}
        //else
        //{
        //    anim.SetFloat("Blend", 0f, 0.1f, Time.deltaTime);
        //}
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
        Instantiate(GuidedRocket, rightRocketSpawn.transform.position, rightRocketSpawn.transform.rotation);
    }
    public void InstantiateRocketLeft()
    {
        Instantiate(GuidedRocket, leftRocketSpawn.transform.position, leftRocketSpawn.transform.rotation);
    }
}
