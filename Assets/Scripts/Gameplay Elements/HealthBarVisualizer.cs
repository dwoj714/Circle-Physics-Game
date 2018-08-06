﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarVisualizer : MonoBehaviour {

	HealthBar source;

	public bool shieldBar;
	public bool mobile;

	PhysCircle parentCircle;

	float initialScaleX;

	//Ok so you needed this to avoid adding localPosition directly from the transform
	//because its values can change slightly during (most likely) physics
	//calculations, and apparently somehow allows floating point imprecision
	//to appear, add up exponentially, and "lock" the health bar directly on top of the parent transform
	private Vector3 initialLocalPosition;

	void Start()
	{
		initialLocalPosition = transform.localPosition;
		initialScaleX = transform.localScale.x;
		source = transform.parent.GetComponent<HealthBar>();
		parentCircle = transform.parent.GetComponent<PhysCircle>();
	}

	void Update ()
	{
		if (mobile)
		{
			transform.rotation = Quaternion.identity;
			transform.position = transform.parent.position + Vector3.up * initialLocalPosition.y;
		}

		if (shieldBar && source.maxShield > 0)
		{
			transform.localScale = new Vector3(source.getShield() / source.maxShield * initialScaleX, transform.localScale.y, transform.localScale.z);
		}
		else
		{
			transform.localScale = new Vector3(source.getHealth() / source.maxHealth * initialScaleX, transform.localScale.y, transform.localScale.z);
		}
	}
}
