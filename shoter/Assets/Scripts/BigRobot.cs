using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRobot : AIBasics
{
    private void Update()
    {
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            anim.SetLayerWeight(1, 0);
            FireMainWeapon();
        }
        else
        {
            anim.SetLayerWeight(1, 1);
        }
    }

    public void FireMainWeapon()
    {
        IEnumerator Shooting()
        {
            anim.SetLayerWeight(2, 1);
            yield return new WaitForSecondsRealtime(2);
            //Instantiate(Rocket);
            //Instantiate(Rocket);
            anim.SetLayerWeight(2, 0);
        }
    }
}
