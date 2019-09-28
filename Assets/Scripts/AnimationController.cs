using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public PlayerController playerController;
    public Animator anim;

    void Start()
    {
        
    }

    void Update()
    {
        anim.SetFloat("Speed", playerController.rb.velocity.magnitude);
    }
}