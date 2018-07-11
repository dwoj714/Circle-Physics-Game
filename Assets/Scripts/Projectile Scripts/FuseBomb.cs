﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Detonator))]
public class FuseBomb : Projectile {

	[HideInInspector]
	public Detonator detonator;

	protected override void Awake()
	{
		base.Awake();
		detonator = GetComponent<Detonator>();
	}

	protected void Start()
	{
		//base.Start();
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
