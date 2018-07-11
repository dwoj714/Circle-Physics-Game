using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow1D : MonoBehaviour {

	public float upperBound;
	public float lowerBound;

	public float pushRange;

	float zPosition;

	float offsetY;

	PlayerController player;
	Rigidbody2D rb;

	// Use this for initialization
	void Awake ()
	{
		player = GameObject.Find("Player").GetComponent<PlayerController>();
		rb = player.GetComponent<Rigidbody2D>();
		zPosition = transform.position.z;
	}
	
	void Update ()
	{
		if (Mathf.Abs(transform.position.y - player.transform.position.y) > pushRange)
		{
			float yCoord = player.transform.position.y + pushRange * (player.transform.position.y > transform.position.y ? -1 : 1);
			transform.position = Vector3.up * Mathf.Clamp(yCoord, lowerBound, upperBound) + Vector3.forward * zPosition;
		}
	}


}
