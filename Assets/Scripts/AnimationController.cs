using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public PlayerController playerController;
    public Animator anim;
    public enum PlayerMovementType { Normal, Strafe, Backpedal };
    PlayerMovementType playerMovementType = PlayerMovementType.Normal;



    void Start()
    {
        
    }

    void Update()
    {
        DetermineMovementType();
        anim.SetFloat("Speed", playerController.rb.velocity.magnitude);
    }

    public void PlayThrowBombAnimation()
    {
        anim.Play("Throw_Bomb");
    }

    void DetermineMovementType()
    {
        Vector3 dir = playerController.rb.velocity;
        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //print(angle);
    }
}