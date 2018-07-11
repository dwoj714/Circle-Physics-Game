using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Detonator))]
public class ImpactBomb : Projectile
{
	[HideInInspector]
	public Detonator detonator;

	protected override void Awake()
	{
		base.Awake();
		detonator = GetComponent<Detonator>();
	}

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
