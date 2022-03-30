using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Audio;

public class MediumRobot : AIBasics
{
    float damping = 1f;
    public float AttackReloadTime;
    bool reloading;

    public Transform bulletSpawn;

    [Header("Audio")]
    public AudioClip footstep;
    public override void Update()
    {
        base.Update();

        if (!reloading)
        {
            nav.speed = moveSpeed;
            if (Vector3.Distance(targetDestination.position, transform.position) <= nav.stoppingDistance)
            {
                nav.speed = 0;

                FireMainWeapon();
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

    public void FireMainWeapon()
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
            yield return new WaitForSecondsRealtime(AttackReloadTime);
            reloading = false;
        }
    }

    //SOUNDS

    public void PlayFootstep()
    {
        audio.PlayOneShot(footstep);
    }
}
