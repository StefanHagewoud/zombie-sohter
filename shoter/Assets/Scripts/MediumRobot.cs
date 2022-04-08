using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MediumRobot : AIBasics
{
    float damping = 1f;
    public float AttackReloadTime;
    bool reloading;

    public Transform[] bulletSpawns;

    [Header("Audio")]
    public AudioClip footstep;
    public override void Update()
    {
        base.Update();

        if (Vector3.Distance(targetDestination.position, transform.position) <= nav.stoppingDistance)
        {
            anim.SetFloat("Blend", 1f, 0.1f, Time.deltaTime);
            var lookPos = targetDestination.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            anim.SetLayerWeight(anim.GetLayerIndex("Shooting"), 1);
        }
        else
        {
            anim.SetFloat("Blend", 0f, 0.1f, Time.deltaTime);
            anim.SetLayerWeight(anim.GetLayerIndex("Shooting"), 0);
        }
    }

    public void Shoot()
    {
        RaycastHit Hit;
        foreach(Transform bulletSpawn in bulletSpawns)
        {
            bulletSpawn.GetComponent<ParticleSystem>().Play();

            if (Physics.Raycast(bulletSpawn.transform.position, targetDestination.position, out Hit, 10f))
            {
                Debug.Log(Hit.collider.name);

                if (Hit.collider.CompareTag("Player"))
                {
                    Hit.collider.GetComponent<Health>().GetHit(damage);
                }
            }
        }
    }


    //SOUNDS

    public void PlayFootstep()
    {
        //audioS.PlayOneShot(footstep);
    }
}
