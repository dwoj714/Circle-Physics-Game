using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow1D : MonoBehaviour {

	Rigidbody2D followRB;

	//public bool vertical = true;

	public float upperBound;
	public float lowerBound;

	public float pushRange;

	private float zPosition;

	// Use this for initialization
	void Start ()
	{
		followRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		zPosition = transform.position.z;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = Vector3.up * Mathf.Clamp(followRB.position.y, lowerBound, upperBound) + Vector3.forward * zPosition;
	}


}
