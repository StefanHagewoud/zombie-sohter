using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRobot : AIBasics
{
    public GameObject GuidedRocket;

    public GameObject rightRocketSpawn;
    public GameObject leftRocketSpawn;

    public override void Update()
    {
        base.Update();

        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Movement"), 0);
            FireMainWeapon();
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Movement"), 1);
        }
    }

    public void FireMainWeapon()
    {
        IEnumerator Shooting()
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Shooting"), 1);
            yield return new WaitForSecondsRealtime(2);
            anim.SetLayerWeight(anim.GetLayerIndex("Shooting"), 0);
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
