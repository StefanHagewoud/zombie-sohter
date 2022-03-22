using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plakband : MonoBehaviour
{
    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Playanimation()
    {
        animator.SetBool("Plakband", true);
        Invoke("CloseAnimation", 11);
    }
    
    public void CloseAnimation()
    {
        animator.SetBool("Plakband", false);
    }
}
