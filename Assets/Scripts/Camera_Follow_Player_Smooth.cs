using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow_Player_Smooth : MonoBehaviour
{
	public float smoothTime = 0.3f;
	public float cameraHeight = 15;
	public float cameraDistance = -8;
	public float xCameraRotation = 75;

    private Vector3 velocity;

    public Transform target;



	void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y + cameraHeight, target.position.z + cameraDistance);
        transform.eulerAngles = new Vector3(xCameraRotation, 0, 0);
    }
	
	void Update() 
	{
        UpdateCameraPos();
	}

    void UpdateCameraPos()
    {
        Vector3 offset = new Vector3(0, cameraHeight, cameraDistance);
        Vector3 goalPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);
        transform.eulerAngles = new Vector3(xCameraRotation, 0, 0);
    }
}