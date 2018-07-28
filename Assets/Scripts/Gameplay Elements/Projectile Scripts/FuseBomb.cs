using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Detonator))]
public class FuseBomb : ExplosiveProjectile {

	protected void Start()
	{
		detonator.sparked = true;
	}

	//Same as the base collision code, without destroying the projectile
	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		HealthBar target = collision.collider.GetComponent<HealthBar>();
		if (target)
		{
			target.takeDamage(impactDMG);
		}
	}

	void OnExplosion()
	{
		GameObject.Destroy(this.gameObject);
	}
}
