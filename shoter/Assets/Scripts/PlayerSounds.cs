using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip footStep;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void PlayFootStep()
    {
        audioS.PlayOneShot(footStep);
    }
}
