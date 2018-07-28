using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Detonator))]
public abstract class ExplosiveProjectile : Projectile {

	[HideInInspector]
	public Detonator detonator;

	protected override void Awake()
	{
		base.Awake();
		detonator = GetComponent<Detonator>();
	}

	protected void onExplosion()
	{
		GameObject.Destroy(this.gameObject);
	}
}
