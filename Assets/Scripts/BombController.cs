using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Detonator))]
public class BombController : HealthBarObject
{
	public float fallSpeed;

	[HideInInspector]
	public Rigidbody2D rb;

	private Detonator detonator;

	private float gravity;

	protected override void Start()
	{
		base.Start();
		gravity = rb.gravityScale;
	}

	protected void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		detonator = GetComponent<Detonator>();
	}

	void FixedUpdate()
	{

		//This ensures the ball will always accellerate or decellerate toward
		//a certain fixed fall speed via velocity damping
		if(rb.velocity.y < -fallSpeed)
		{
			rb.gravityScale = 0;
		}
		else
		{
			rb.gravityScale = gravity;
		}
	}

	public override void die()
	{
		detonator.sparked = true;
	}

	void OnExplosion()
	{
		GameObject.Destroy(this.gameObject);
	}
}