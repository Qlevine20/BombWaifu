using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform bombThrowLocation;
    public LayerMask interactionMask;
    public Collider myCollider;

	public float moveSpeed;
	public Rigidbody rb;

	private Vector3 moveInput;

	private Camera mainCamera;


    
	void Start ()
    {
		//rb = GetComponent<Rigidbody>();
		mainCamera = FindObjectOfType<Camera>();
	}

	void Update ()
    {
		moveInput = new Vector3 (Input.GetAxisRaw("Horizontal") * moveSpeed, 0f, Input.GetAxisRaw("Vertical") * moveSpeed);
        //rb.velocity = moveInput;



        //Player rotation stuff

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
		float rayLength;

		if (groundPlane.Raycast(cameraRay, out rayLength))
        {
			Vector3 pointToLook = cameraRay.GetPoint(rayLength);
			Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);
			transform.LookAt(new Vector3 (pointToLook.x, transform.position.y, pointToLook.z));
		}



        //Bomb throwing
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, interactionMask))
            {
                Bomb newBomb = BombPool.Instance.Get();
                newBomb.transform.position = bombThrowLocation.position;
                newBomb.transform.rotation = bombThrowLocation.rotation;
                newBomb.transform.localScale = bombThrowLocation.localScale;
                newBomb.gameObject.SetActive(true);
                newBomb.Throw(hit.point, myCollider);
            }
        }
	}
    
	void FixedUpdate()
    {
        rb.velocity = moveInput;
    }
}