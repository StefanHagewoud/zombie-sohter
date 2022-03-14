using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRobot : AIBasics
{
    public float AttackCooldown;
    bool cooldown;

    float damping = 2f;

    bool attacking; public override void Update()
    {
        base.Update();

        if (!cooldown)
        {
            nav.speed = moveSpeed;
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                nav.speed = 0;

                MainAttack();
            }

        }

        if (nav.remainingDistance <= nav.stoppingDistance)
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
        StartCoroutine(Attack());
        IEnumerator Attack()
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Melee"), 1);
            anim.SetLayerWeight(anim.GetLayerIndex("Movement"), 0);
            yield return new WaitForSecondsRealtime(4);
            anim.SetLayerWeight(anim.GetLayerIndex("Melee"), 0);
            anim.SetLayerWeight(anim.GetLayerIndex("Movement"), 1);
            StartCoroutine(Cooldown());
        }
        IEnumerator Cooldown()
        {
            cooldown = true;
            yield return new WaitForSecondsRealtime(AttackCooldown);
            cooldown = false;
        }
    }
}
