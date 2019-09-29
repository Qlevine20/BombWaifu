﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform bombThrowLocation;
    public float bombThrowCooldown;
    private float bombThrowTimer;
    public LayerMask interactionMask;
    public Collider myCollider;

	public Rigidbody rb;

    AudioSource audioSource;
    public AudioClip throwBombClip;

    private Vector3 velocity;
    public float acceleration;
    public float maxVelocity;

    private bool stoppingZ = false;
    private bool stoppingX = false;
    public float stopTime;

	public Camera mainCamera;

    private bool hasHeartUpgrade = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        bombThrowTimer = bombThrowCooldown;
    }

	void Update ()
    {
        if(hasHeartUpgrade)
        {
            if(heartUpgradTimer >= heartUpgradTime)
            {
                hasHeartUpgrade = false;
                heartUpgradTimer = 0;
            }
            heartUpgradTimer += Time.deltaTime;
        }
		//moveInput = new Vector3 (Input.GetAxisRaw("Horizontal") * moveSpeed, 0f, Input.GetAxisRaw("Vertical") * moveSpeed);
        if(Input.GetKey(KeyCode.W))
        {
            AddVelocity(ref velocity.z, acceleration);
            stoppingZ = false;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            AddVelocity(ref velocity.z, -acceleration);
            stoppingZ = false;
        }
        else
            StartCoroutine(ResetZVelocity());

        if(Input.GetKey(KeyCode.A))
        {
            AddVelocity(ref velocity.x, -acceleration);
            stoppingX = false;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            AddVelocity(ref velocity.x, acceleration);
            stoppingX = false;
        }
        else
            StartCoroutine(ResetXVelocity());

        //Player rotation stuff
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit rotationHit;
        if(Physics.Raycast(cameraRay, out rotationHit, Mathf.Infinity))
        {
			//Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Vector3 pointToLook = rotationHit.point;
			Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);
			transform.LookAt(new Vector3 (pointToLook.x, transform.position.y, pointToLook.z));
		}

        //Bomb throwing
        if (Input.GetMouseButtonDown(0))
        {
            if(bombThrowTimer >= bombThrowCooldown)
            {
                audioSource.clip = throwBombClip;
                audioSource.Play();

                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, interactionMask))
                {
                    Bomb newBomb;

                    if(hasHeartUpgrade)
                        newBomb = HeartBombPool.Instance.Get();
                    else
                        newBomb = PotatoBombPool.Instance.Get();

                    newBomb.transform.position = bombThrowLocation.position;
                    newBomb.transform.rotation = bombThrowLocation.rotation;
                    newBomb.transform.localScale = bombThrowLocation.localScale;
                    newBomb.gameObject.SetActive(true);
                    newBomb.Throw(hit.point, myCollider);
                }
                bombThrowTimer = 0;
            }
        }
        bombThrowTimer += Time.deltaTime;
	}

    private void AddVelocity(ref float velAxis, float accel)
    {
        velAxis = Mathf.Clamp(velAxis + (accel * Time.deltaTime), -maxVelocity, maxVelocity);
    }
    
	void FixedUpdate()
    {
        rb.velocity = velocity;
    }

    public IEnumerator ResetZVelocity()
    {
        float t = Mathf.Abs(velocity.z) * stopTime;
        float startZVel = velocity.z;
        stoppingZ = true;
        while(stoppingZ)
        {
            if(velocity.z == 0)
                stoppingZ = false;
            velocity.z = Mathf.Lerp(0, startZVel, t);
            t -= Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator ResetXVelocity()
    {
        float t = Mathf.Abs(velocity.x) * stopTime;
        float startXVel = velocity.x;
        stoppingX = true;
        while(stoppingX)
        {
            if(velocity.x == 0)
                stoppingX = false;
            velocity.x = Mathf.Lerp(0, startXVel, t);
            t -= Time.deltaTime;
            yield return null;
        }
    }

    void OnCollisionEnter(Collision other) {
        StartCoroutine(ResetZVelocity());
        StartCoroutine(ResetXVelocity());
    }

    public float heartUpgradTime;
    private float heartUpgradTimer = 0;

    public void GainHeartUpgrade()
    {
        hasHeartUpgrade = true;
    }
}