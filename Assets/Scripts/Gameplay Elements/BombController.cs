using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBar))]
[RequireComponent(typeof(Detonator))]
[RequireComponent(typeof(DropPool))]
public class BombController : PhysCircle
{
	public float fallSpeed;

	Detonator detonator;

	public float gravity;

	DropPool loot;

	protected override void Awake()
	{
		base.Awake();
		rb = GetComponent<Rigidbody2D>();
		detonator = GetComponent<Detonator>();
		gravity = rb.gravityScale;
		loot = GetComponent<DropPool>();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		
		//This ensures the ball will always accelerate or decelerate toward
		//a certain fixed fall speed via velocity damping
		if(rb.velocity.y < -fallSpeed || (fallSpeed < 0 && rb.velocity.y > -fallSpeed ))
		{
			rb.gravityScale = 0;
		}
		else
		{
			rb.gravityScale = gravity;
		}
	}

	public void onHealthDeplete()
	{
		detonator.sparked = true;
	}

	void OnExplosion()
	{
		loot.spawnRandom(transform);
		Destroy(this.gameObject);
	}
}