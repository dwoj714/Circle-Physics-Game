using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow1D : MonoBehaviour {

	public float upperBound;
	public float lowerBound;

	public float pushRange;

	public float smoothing = .5f;

	float zPosition;

	float offsetY;

	PlayerController player;
	
    // Use this for initialization
    void Awake ()
	{
		player = GameObject.Find("Player").GetComponent<PlayerController>();
		zPosition = transform.position.z;
	}
	
	void Update ()
	{
		//The y coordinate of the player plus pushRange in the direction of travel
		//float targetY = Mathf.Clamp(rb.velocity.y,-1,1) * pushRange + rb.transform.position.y;

		//transform.position = Vector3.up * Mathf.Clamp((Mathf.Lerp(transform.position.y,targetY,smoothing)),lowerBound,upperBound) + Vector3.forward * zPosition;
		

		if (Mathf.Abs(transform.position.y - player.transform.position.y) > pushRange)
		{
			float yCoord = player.transform.position.y + pushRange * (player.transform.position.y > transform.position.y ? -1 : 1);
			transform.position = Vector3.up * Mathf.Clamp(yCoord, lowerBound, upperBound) + Vector3.forward * zPosition;
		}
	}
}
