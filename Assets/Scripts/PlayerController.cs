﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(ProjectileHandler))]
public class PlayerController : MonoBehaviour
{
	Rigidbody2D rb;
	CircleCollider2D col;

	//The largest allowed mouse drag length.
	//Used to calculate what percentage of power to shoot/move with
	public float maxMagnitude;

	public float minSpeed, maxSpeed;

	//Hold the location of the mouse where the respective buttons were pressed
	Vector2 mouseHolder0, mouseHolder1;

	//Holds the location of the mouse while holding respective mouse buttons
	[HideInInspector]
	public Vector2 mouseDrag0, mouseDrag1;

	ProjectileHandler weapon;

	protected void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<CircleCollider2D>();
		weapon = GetComponent<ProjectileHandler>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
		{
			Time.timeScale = .15f;
		}
		else
		{
			Time.timeScale = 1;
		}

		//Store the position of the mouse when initiating a mouse drag
		if (Input.GetMouseButtonDown(0))
		{
			mouseHolder0 = Input.mousePosition;
		}
		if (Input.GetMouseButtonDown(1))
		{
			mouseHolder1 = Input.mousePosition;
		}

		//Retrieve the position of the mouse while dragging, draw a ray showing the drag
		if (Input.GetMouseButton(0))
		{
			mouseDrag0 = Vector2.ClampMagnitude(((Vector2)Input.mousePosition - mouseHolder0) / -5, maxMagnitude);
			Debug.DrawRay(transform.position, mouseDrag0, Color.green);
		}
		if (Input.GetMouseButton(1))
		{
			mouseDrag1 = Vector2.ClampMagnitude(((Vector2)Input.mousePosition - mouseHolder1) / -5, maxMagnitude);
			Debug.DrawRay(transform.position, mouseDrag1, Color.red);
		}

		//On mouse release, move the player or fire projectiles
		if (Input.GetMouseButtonUp(0))
		{
			float speed = mouseDrag0.magnitude / maxMagnitude * (maxSpeed - minSpeed) + minSpeed;
			rb.velocity = mouseDrag0.normalized * speed;
			mouseDrag0 = Vector2.zero;
		}
		if (Input.GetMouseButtonUp(1))
		{
			weapon.fire(mouseDrag1, mouseDrag1.magnitude / maxMagnitude);
			mouseDrag1 = Vector2.zero;
		}
	}
}
