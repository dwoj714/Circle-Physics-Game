using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(CircleCollider2D))]
public class Projectile : PhysCircle
{
	public bool hasFixedSpeed;

	//Used as the initial magnitude of velocity
	[HideInInspector]
	public float speed;

	//Used to constrain speed (above) to a certain range when it's calculated
	public float minSpeed;
	public float maxSpeed;

	[HideInInspector]
	//The GameObject that fired the projectile
	public GameObject owner;

	public float energyCost = 10;

	protected override void Awake()
	{
		base.Awake();
		rb = GetComponent<Rigidbody2D>();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (hasFixedSpeed)
			rb.velocity = rb.velocity.normalized * speed;
	}

}
