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

	public float impactDMG;

	[HideInInspector]
	//The GameObject that fired the projectile
	public GameObject owner;

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

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		base.OnCollisionEnter2D(collision);

		/*HealthBar target = collision.collider.GetComponent<HealthBar>();
		if (target)
		{
			target.takeDamage(impactDMG);
		}*/

		//GameObject.Destroy(this.gameObject);
	}

	/*You were using these to have the projectile start at the center of the player as a trigger and smoothly move out
	void OnTriggerExit2D(Collider2D col)
	{
		//Debug.Log(col.gameObject.name);
		if (col.gameObject == owner)
		{
			this.col.isTrigger = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col != owner)
		{
			this.col.isTrigger = false;
		}
	}*/
}
