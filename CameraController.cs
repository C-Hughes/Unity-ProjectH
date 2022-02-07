using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject player;
	private float rotateSpeed = 75.0f;
	private float zoomSpeed = 30;
	private float moveSpeed = 9;
	private float maxDistance = 7;

	private float zoomLevel;
	private float sensitivity = 1;
	private float maxZoom = 15;
	private float minZoom = 25;
	float zoomPosition;
	//private Vector3 moveDirection = Vector3.zero;
	private Vector3 initPosition;
	public float distance;
	public float newDistance;

	void Start()
    {
		initPosition = player.transform.position;
	}

	void Update()
	{
		//Zoom
		zoomLevel -= Input.mouseScrollDelta.y * sensitivity;
		zoomLevel = Mathf.Clamp(zoomLevel, maxZoom, minZoom);
		zoomPosition = Mathf.MoveTowards(zoomPosition, zoomLevel, zoomSpeed * Time.deltaTime);
		transform.position = player.transform.position - (transform.forward * zoomPosition);

		//Rotate
		transform.RotateAround(player.transform.position + new Vector3(0, 20, 0), Vector3.up, -Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime);

		//Forward and Backwards
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		{
			newDistance = Vector3.Distance(player.transform.position + new Vector3(transform.forward.x, 0, transform.forward.z) * moveSpeed * Time.deltaTime, initPosition);
			if (distance < maxDistance)
			{
				player.transform.position = player.transform.position + new Vector3(transform.forward.x, 0, transform.forward.z) * moveSpeed * Time.deltaTime;
			}
			else
			{
				if (newDistance < distance)
				{
					player.transform.position = player.transform.position + new Vector3(transform.forward.x, 0, transform.forward.z) * moveSpeed * Time.deltaTime;
				}
			}
		}
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			newDistance = Vector3.Distance(player.transform.position + new Vector3(-transform.forward.x, 0, -transform.forward.z) * moveSpeed * Time.deltaTime, initPosition);
			if (distance < maxDistance)
			{
				player.transform.position = player.transform.position + new Vector3(-transform.forward.x, 0, -transform.forward.z) * moveSpeed * Time.deltaTime;
			} 
			else
            {
				if(newDistance < distance)
                {
					player.transform.position = player.transform.position + new Vector3(-transform.forward.x, 0, -transform.forward.z) * moveSpeed * Time.deltaTime;
				}
            }
		}
		distance = Vector3.Distance(player.transform.position, initPosition);
	}
}
