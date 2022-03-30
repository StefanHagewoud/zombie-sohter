using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRobot : AIBasics
{
    float damping = 1f;
    public float attackCooldown;
    bool cooldown;
    public Transform damagePoint;
    public override void Update()
    {
        base.Update();
        Debug.Log(nav.remainingDistance);
        if (!cooldown)
        {
            if (Vector3.Distance(targetDestination.position, transform.position) <= nav.stoppingDistance)
            {
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
        StartCoroutine(Attacking());
        IEnumerator Attacking()
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Melee"), 1);
            yield return new WaitForSecondsRealtime(0.30f);
            anim.SetLayerWeight(anim.GetLayerIndex("Melee"), 0);
            nav.speed = moveSpeed;
            StartCoroutine(Cooldown());
        }
        IEnumerator Cooldown()
        {
            cooldown = true;
            yield return new WaitForSecondsRealtime(attackCooldown);
            cooldown = false;
        }
    }

    void DoDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(damagePoint.position, 2);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Player")
            {
                hitCollider.GetComponent<Health>().GetHit(damage);
            }
        }
    }
}
