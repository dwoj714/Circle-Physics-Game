using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
	public Projectile ammoType;

	//If the magnitude of direction is less than this, don't fire
	public float deadZone = .05f;

	public void fire(Vector2 direction, float power)
	{
		if(power > deadZone)
		{
			Projectile projectile = GameObject.Instantiate(ammoType, transform.position + Vector3.back, Quaternion.identity);

			float speedRange = ammoType.maxSpeed - ammoType.minSpeed;

			projectile.speed = projectile.minSpeed + speedRange * power;
			projectile.rb.velocity = direction.normalized * projectile.speed;
			projectile.owner = this.gameObject;

			//Move the projectile to the edge of the collider. Take the projectile's radius into consideration as well

			Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
			//I'm assuming anything with a rigidbody will have a circle collider
			if (rb)
			{
				//Greater scales are the greater of the two x/y factors scaling the size of the object.
				//The greater scale is what affects the radius of the circle collider attached to the object.

				//Greater Scale of this object
				Vector2 scale = transform.localScale;
				float greaterScale = scale.x > scale.y ? scale.x : scale.y;

				//Greater Scale of the projectile
				Vector2 pScale = projectile.transform.localScale;
				float pGreaterScale = pScale.x > pScale.y ? pScale.x : pScale.y;

				projectile.transform.position += (Vector3)(rb.velocity.normalized * (gameObject.GetComponent<CircleCollider2D>().radius * greaterScale + projectile.col.radius * pGreaterScale)) * 1.1f;
			}
		}
	}
}
