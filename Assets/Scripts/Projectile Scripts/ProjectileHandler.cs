using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
	CircleCollider2D col;

	public Projectile ammoType;

	//If the magnitude of direction is less than this, don't fire
	public float deadZone = .05f;

	void Start()
	{
		col = GetComponent<CircleCollider2D>();
	}

	public void fire(Vector2 direction, float power)
	{
		direction.Normalize();

		if(power >= deadZone)
		{
			float speedRange = ammoType.maxSpeed - ammoType.minSpeed;
			float speed = ammoType.minSpeed + speedRange * power;

			Projectile projectile = GameObject.Instantiate(ammoType, transform.position, Quaternion.identity);

			projectile.rb.velocity = direction * speed;
			projectile.speed = speed;

		}
	}
}

/*

	float speedRange = ammoType.maxSpeed - ammoType.minSpeed;
	float speed = ammoType.minSpeed + speedRange * power;

	Vector2 scale = transform.localScale;
	float greaterScale = scale.x > scale.y ? scale.x : scale.y;
	scale = ammoType.transform.localScale;
	float pGreaterScale = scale.x > scale.y ? scale.x : scale.y;

	Vector3 offset = (Vector3)(direction * (col.radius * greaterScale + ammoType.col.radius * pGreaterScale));

	Projectile projectile = GameObject.Instantiate(ammoType, transform.position + offset, Quaternion.identity);
	projectile.rb.velocity = direction * speed;
	projectile.speed = speed;

*/


/*
			float speedRange = ammoType.maxSpeed - ammoType.minSpeed;
			float speed = ammoType.minSpeed + speedRange * power;

			//If the object firing the projectile has a rigidbody, add it to the velocity
			Vector2 velocity = direction * speed + (rb ? rb.velocity : Vector2.zero);

			//Instasntiate and initialize the projectile
			Projectile projectile = GameObject.Instantiate(ammoType, transform.position + Vector3.back, Quaternion.identity);

			

			projectile.speed = projectile.minSpeed + speedRange * power;
			projectile.rb.velocity = velocity;
			projectile.owner = gameObject;
			




			/*
			
			//Greater scales are the greater of the two x/y factors scaling the size of the object.
			//The greater scale is what affects the radius of the circle collider attached to the object.

			//Greater Scale of this object
			Vector2 scale = transform.localScale;
			float greaterScale = scale.x > scale.y ? scale.x : scale.y;

			//Greater Scale of the projectile
			Vector2 pScale = projectile.transform.localScale;
			float pGreaterScale = pScale.x > pScale.y ? pScale.x : pScale.y;

			projectile.transform.position += (Vector3)(projectile.rb.velocity.normalized * (gameObject.GetComponent<CircleCollider2D>().radius * greaterScale + projectile.col.radius * pGreaterScale));
			*/

/*
		//Instasntiate and initialize the projectile
		Projectile projectile = GameObject.Instantiate(ammoType, transform.position + Vector3.back, Quaternion.identity);

		float speedRange = ammoType.maxSpeed - ammoType.minSpeed;

		projectile.speed = projectile.minSpeed + speedRange * power;
		projectile.rb.velocity = direction.normalized * projectile.speed;
		projectile.owner = this.gameObject;

		//Greater scales are the greater of the two x/y factors scaling the size of the object.
		//The greater scale is what affects the radius of the circle collider attached to the object.

		//Greater Scale of this object
		Vector2 scale = transform.localScale;
		float greaterScale = scale.x > scale.y ? scale.x : scale.y;

		//Greater Scale of the projectile
		Vector2 pScale = projectile.transform.localScale;
		float pGreaterScale = pScale.x > pScale.y ? pScale.x : pScale.y;

		Vector3 offset = (Vector3)(projectile.rb.velocity.normalized * (gameObject.GetComponent<CircleCollider2D>().radius * greaterScale + projectile.col.radius * pGreaterScale));

		Debug.Log("Offset: " + offset + "\nVelocity: " + projectile.rb.velocity);

		projectile.transform.position += offset;
*/
