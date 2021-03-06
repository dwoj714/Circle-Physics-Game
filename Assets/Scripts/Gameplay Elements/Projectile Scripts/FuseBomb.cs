﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Detonator))]
public class FuseBomb : ExplosiveProjectile {

	protected virtual void Start()
	{
		detonator.sparked = true;
	}

	void OnExplosion()
	{
		GameObject.Destroy(this.gameObject);
	}
}
