using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRobot : AIBasics
{
    float damping = 1f;
    public float MagReloadTime;
    bool reloading;
    public override void Update()
    {
        base.Update();
        Debug.Log(nav.remainingDistance);
        if (!reloading)
        {
            nav.speed = moveSpeed;
            if (Vector3.Distance(targetDestination.position, transform.position) <= nav.stoppingDistance)
            {
                nav.speed = 0;
                MainAttack();
            }
        }

        if (Vector3.Distance(targetDestination.position, transform.position) <= nav.stoppingDistance)
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

    public void MainAttack()
    {
        StartCoroutine(Shooting());
        IEnumerator Shooting()
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Shooting"), 1);
            yield return new WaitForSecondsRealtime(2f);
            anim.SetLayerWeight(anim.GetLayerIndex("Shooting"), 0);
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
