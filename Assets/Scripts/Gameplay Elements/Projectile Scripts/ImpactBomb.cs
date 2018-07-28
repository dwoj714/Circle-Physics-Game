using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactBomb : ExplosiveProjectile
{
	//On collision, deal impact damage and spark the detonator
	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		HealthBar target = collision.collider.GetComponent<HealthBar>();
		if (target)
		{
			target.takeDamage(impactDMG);
		}
		detonator.sparked = true;
	}

	void OnExplosion()
	{
		GameObject.Destroy(this.gameObject);
	}
}
