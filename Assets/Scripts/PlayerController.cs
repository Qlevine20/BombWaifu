using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed;
	private Rigidbody rb;

	private Vector3 moveInput;

	private Camera mainCamera;


    
	void Start ()
    {
		rb = GetComponent<Rigidbody>();
		mainCamera = FindObjectOfType<Camera>();
	}

	void Update ()
    {
		moveInput = new Vector3 (Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        //Raycast stuff
        Ray cameraRay = mainCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
		float rayLength;

		if (groundPlane.Raycast(cameraRay, out rayLength))
        {
			Vector3 pointToLook = cameraRay.GetPoint (rayLength);
			Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);
			transform.LookAt(new Vector3 (pointToLook.x, transform.position.y, pointToLook.z));
		}
	}
    
	void FixedUpdate ()
    {
		
	}
}
