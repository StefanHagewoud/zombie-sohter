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

        if (nav.remainingDistance <= nav.stoppingDistance && !reloading)
        {
            nav.speed = 0;
            FireMainWeapon();
        }
        if (!reloading)
        {
            nav.speed = moveSpeed;
        }
    }

    public void FireMainWeapon()
    {
        StartCoroutine(Shooting());
        IEnumerator Shooting()
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Movement"), 0);
            anim.SetLayerWeight(anim.GetLayerIndex("Shooting"), 1);
            yield return new WaitForSecondsRealtime(0.58f);
            anim.SetLayerWeight(anim.GetLayerIndex("Shooting"), 0);
            anim.SetLayerWeight(anim.GetLayerIndex("Movement"), 1);
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
