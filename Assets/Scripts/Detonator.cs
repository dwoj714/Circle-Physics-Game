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
	CircleCollider2D col;

	void Awake()
	{
		fuseTimer = fuse;
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
		Collider2D[] results = Physics2D.OverlapCircleAll(col.transform.position, explosionRadius);

		if (results.Length > 0)
		{
			//declare a rigidbody to reference the rigidbodies of the detected colliders
			Rigidbody2D hitRb;
			HealthBarObject hitHB;

			float pushForce;
			float damage;

			foreach (Collider2D hitCol in results)
			{
				//set hitRb to the rigidbody of the current collider, if it exists
				hitRb = hitCol.gameObject.GetComponent<Rigidbody2D>();

				//set hitHB to the HealthBarObject of the current collider, if it exists
				hitHB = hitCol.gameObject.GetComponent<HealthBarObject>();

				float forceRange = maxPushForce - minPushForce;

				float dmgRange = maxExplosionDMG - minExplosionDMG;

				if (hitCol != col)
				{
					ColliderDistance2D cd = col.Distance(hitCol);

					//The greater of the two factors (X and Y) scaling the gameObject. This is what scales the radius of the
					//circle collider. Taking this into account ensures consistency when altering the transform's scale.
					Vector3 scale = transform.localScale;
					float greaterScale = scale.x > scale.y ? scale.x : scale.y;

					//adjusted collider distance. Edge of target collider to transform of exploding collider
					float dst = Mathf.Clamp(cd.distance, 0, Mathf.Infinity);// + col.radius * greaterScale;

					//Adjusted explosion radius. Ensures colliders in contact with the explosion surface
					//recieve exactly max explosion force/damage and things on the edge recieve exactly the minimum.
					float adjustedRadius = explosionRadius - (col.radius * greaterScale);

					//Add forces to colliders with rigidbodies
					if (hitRb)
					{
						pushForce = (adjustedRadius - dst) / adjustedRadius * (forceRange) + minPushForce;
						hitRb.AddForce(cd.normal * pushForce * (cd.distance < 0 ? 1 : -1), ForceMode2D.Impulse);
					}

					//deal damage to colliders attached to HealthBarObjects
					if (hitHB)
					{
						damage = (adjustedRadius - dst) / adjustedRadius * (dmgRange) + minExplosionDMG;
						hitHB.takeDamageLate(damage);
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
