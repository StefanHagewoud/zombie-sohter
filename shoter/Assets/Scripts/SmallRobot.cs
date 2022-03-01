using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRobot : AIBasics
{
    float AttackCooldown;
    bool attacking;
    public void MainAttack()
    {
        StartCoroutine(Attack());
        IEnumerator Attack()
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Melee"), 0);
            anim.SetLayerWeight(anim.GetLayerIndex("Movement"), 0);
            yield return new WaitForSecondsRealtime(4);
            anim.SetLayerWeight(anim.GetLayerIndex("Melee"), 0);
            anim.SetLayerWeight(anim.GetLayerIndex("Movement"), 0);
            StartCoroutine(Cooldown());
        }
        IEnumerator Cooldown()
        {
            yield return new WaitForSecondsRealtime(AttackCooldown);
        }
    }
}
