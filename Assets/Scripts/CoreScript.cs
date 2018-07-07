using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScript : HealthBarObject {

	CircleCollider2D col;

	public Detonator burner;

	// Use this for initialization
	protected override void Start ()
	{
		base.Start();
		col = GetComponent<CircleCollider2D>();
	}

	public override void die()
	{
		throw new NotImplementedException();
	}

	void FixedUpdate()
	{
		burner.explode();
	}
}
