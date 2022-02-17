using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRobot : AIBasics
{
    public GameObject GuidedRocket;

    public GameObject rightRocketSpawn;
    public GameObject leftRocketSpawn;

    bool shooting;
    public override void Update()
    {
        base.Update();

        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            shooting = true;
            FireMainWeapon();
        }
        else
        {
            
        }
    }

    public void FireMainWeapon()
    {
        StartCoroutine(Shooting());
        IEnumerator Shooting()
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Movement"), 0);
            anim.SetLayerWeight(anim.GetLayerIndex("Shooting"), 1);
            yield return new WaitForSecondsRealtime(2);
            anim.SetLayerWeight(anim.GetLayerIndex("Shooting"), 0);
            anim.SetLayerWeight(anim.GetLayerIndex("Movement"), 1);
            shooting = false;
        }
    }
    public void InstantiateRocketRight()
    {
        Instantiate(GuidedRocket, rightRocketSpawn.transform.position, Quaternion.identity);
    }
    public void InstantiateRocketLeft()
    {
        Instantiate(GuidedRocket, leftRocketSpawn.transform.position, Quaternion.identity);
    }
}
