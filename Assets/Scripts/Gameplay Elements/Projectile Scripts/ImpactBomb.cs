using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactBomb : ExplosiveProjectile
{
	//On collision, deal impact damage and spark the detonator
	protected override void OnCollisionEnter2D(Collision2D hit)
	{
		base.OnCollisionEnter2D(hit);
		detonator.sparked = true;
		rb.drag *= 1.5f;
	}

	void OnExplosion()
	{
		GameObject.Destroy(this.gameObject);
	}
}
