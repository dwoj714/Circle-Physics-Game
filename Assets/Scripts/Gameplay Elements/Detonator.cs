using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour {

	public float minPushForce, maxPushForce;
	public float minExplosionDMG, maxExplosionDMG;
	public float explosionRadius;

	public float fuse = .5f;
	float fuseTimer;

	//When this is true, the fuse is shortened via deltaTime
	public bool sparked;

	[HideInInspector]
	PhysCircle circle;

	CircleCollider2D col;

	void Awake()
	{
		fuseTimer = fuse;
		circle = GetComponent<PhysCircle>();
		col = GetComponent<CircleCollider2D>();
	}

	void Update()
	{
		if (sparked)
		{
			fuseTimer -= Time.deltaTime;
			
			if(fuseTimer <= 0)
			{
				explode();
				sparked = false;
				fuseTimer = fuse;
			}
		}
	}

	public void explode()
	{
		//When exploding, check for colliders within the blast radius
		Collider2D[] results = Physics2D.OverlapCircleAll(circle.transform.position, explosionRadius);

		if (results.Length > 0)
		{
			//declare a rigidbody to reference the rigidbodies of the detected colliders
			Rigidbody2D hitRb;
			HealthBar hitHb;

			float pushForce;
			float damage;

			foreach (Collider2D hitCol in results)
			{
				//set hitRb to the rigidbody of the current collider, if it exists
				hitRb = hitCol.gameObject.GetComponent<Rigidbody2D>();

				//set hitHB to the HealthBar of the current collider, if it exists
				hitHb = hitCol.gameObject.GetComponent<HealthBar>();

				//The ranges of force/damage that can be applied to explosion targets, max at the edge of the collider, min at the edge of the explosion
				float forceRange = maxPushForce - minPushForce;
				float dmgRange = maxExplosionDMG - minExplosionDMG;

				if (hitCol != circle.col)
				{
					ColliderDistance2D cd = circle.col.Distance(hitCol);

					//Clamp the collider distance to positive numbers to avoid issues related to negative collider distance
					float dst = Mathf.Clamp(cd.distance, 0, Mathf.Infinity);

					//Tbh, don't remember what exact purpose this had for explosion force/damage calculations
					float adjustedRadius = explosionRadius - circle.radius();

					//Add forces to colliders with rigidbodies
					if (hitRb)
					{
						pushForce = (adjustedRadius - dst) / adjustedRadius * (forceRange) + minPushForce;
						hitRb.AddForce(cd.normal * pushForce * (cd.distance < 0 ? 1 : -1), ForceMode2D.Impulse);
						Debug.Log(pushForce);
					}

					//deal damage to colliders attached to HealthBars
					if (hitHb)
					{
						damage = (adjustedRadius - dst) / adjustedRadius * (dmgRange) + minExplosionDMG;
						hitHb.takeDamage(damage);
					}
				}
			}
		}
		SendMessage("OnExplosion");
	}

	void OnDrawGizmos()
	{
		if (sparked)
		{
			Gizmos.DrawWireSphere(transform.position, explosionRadius);
		}
	}
}
