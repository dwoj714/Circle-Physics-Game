using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PhysCircle : MonoBehaviour {

	public static float globalDamageMultiplier = 0.65f;

	[HideInInspector]
	public CircleCollider2D col;

	[HideInInspector]
	public Rigidbody2D rb;

	bool collision = false;

	[HideInInspector]
	public Vector2 oldVelocity;

	[HideInInspector]
	public Dictionary<PhysCircle, float> hitRegistry = new Dictionary<PhysCircle, float>();

	private HealthBar hb;

	int frameCount = 0;

	protected virtual void Awake()
	{
		col = GetComponent<CircleCollider2D>();
		rb = GetComponent<Rigidbody2D>();
		hb = GetComponent<HealthBar>();
	}

	protected virtual void FixedUpdate()
	{
		//Used to save the velocity for use during a collision in the same physics update
		oldVelocity = rb.velocity;
	}

	//Returns the radius of the collider adjusted by how it's scaled
	public float adjustedRadius()
	{
		float greaterScale = transform.lossyScale.x > transform.lossyScale.y ? transform.lossyScale.z : transform.lossyScale.y;
		return col.radius * greaterScale;
	}

	protected virtual void OnCollisionEnter2D(Collision2D hit)
	{
		//The PhysCircle we just collided with
		PhysCircle hitCircle = hit.gameObject.GetComponent<PhysCircle>();

		//If the hitCircle exists...
		if (hitCircle /*&& !hitCircle.hitList.Contains(this)*/)
		{
			//Represents the position of hitCircle relative to this physcircle
			Vector2 relativePosition = hitCircle.rb.position - rb.position;

			//The magnitude of the velocity vector projected onto a vector between both PhysCircles centers
			float VPM = oldVelocity.magnitude * Mathf.Cos(Mathf.Deg2Rad * Vector2.Angle(relativePosition, oldVelocity));

			//Add this circle and its VPM to the hit PhysCircles hit registry
			hitCircle.hitRegistry.Add(this, VPM);

			//If we have the other circle in our hit registry, use the VPM associated with it
			//to deal collision damage to both PhysCircles' health bars; whichever ones have them
			if (hitRegistry.ContainsKey(hitCircle))
			{
				float massTotal = rb.mass + hitCircle.rb.mass;
				float collisionVPM = VPM + hitRegistry[hitCircle];

				//The objects' combined masses times their combined VPMs
				float baseDamage = massTotal * collisionVPM;

				//Deal damage to any existing health bars
				if (hb)
				{
					hb.takeDamage(globalDamageMultiplier * baseDamage * (hitCircle.rb.mass / massTotal));
				}
				if (hitCircle.hb)
				{
					hitCircle.hb.takeDamage(globalDamageMultiplier * baseDamage * (rb.mass / massTotal));
				}
			}
		}
		else //If the collision is with terrain (or any other non-PhysCircle collider)
		{
			if (hb)
			{
				ContactPoint2D[] points = new ContactPoint2D[1];
				hit.GetContacts(points);

				Vector2 normal = points[0].normal;
				float VPM = oldVelocity.magnitude * Mathf.Cos(Mathf.Deg2Rad * Vector2.Angle(normal, oldVelocity));

				float baseDamage = Mathf.Abs(VPM) * rb.mass;

				hb.takeDamage(baseDamage * globalDamageMultiplier);
			}
		}
	}

	protected void OnCollisionExit2D(Collision2D hit)
	{
		PhysCircle hitCircle = hit.gameObject.GetComponent<PhysCircle>();

		if (hitCircle)
		{
			hitRegistry.Remove(hitCircle);
		}
	}

}
