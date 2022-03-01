using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumRobot : AIBasics
{
    public float MagReloadTime;
    bool reloading;
    public override void Update()
    {
        base.Update();
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
            yield return new WaitForSecondsRealtime(MagReloadTime);
            reloading = false;
        }
    }
}
