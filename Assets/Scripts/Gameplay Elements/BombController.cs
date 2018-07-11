using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBar))]
[RequireComponent(typeof(Detonator))]
public class BombController : PhysCircle
{
	public float fallSpeed;

	//[HideInInspector]
	///public Rigidbody2D rb;

	private Detonator detonator;

	private float gravity;

	protected override void Awake()
	{
		base.Awake();
		rb = GetComponent<Rigidbody2D>();
		detonator = GetComponent<Detonator>();
		gravity = rb.gravityScale;
	}


	void FixedUpdate()
	{

		//This ensures the ball will always accelerate or decelerate toward
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

	public void onHealthDeplete()
	{
		detonator.sparked = true;
	}

	void OnExplosion()
	{
		GameObject.Destroy(this.gameObject);
	}
}