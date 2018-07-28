using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : Projectile {

	public Vector2 initialVelocity;

	public void Start()
	{
		rb.velocity = initialVelocity;
		speed = initialVelocity.magnitude;
	}


	
}
