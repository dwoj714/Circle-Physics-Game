using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyItem : Buff
{ 
	public float energyBonus = 50;

	public override bool applyEffect(Buffable target)
	{
		//Attempt to get a reference to the ProjectileHandler of the object we're giving the energy to
		ProjectileHandler handler = target.GetComponent<ProjectileHandler>();

		//If handler exists, add energyBonus to its energy pool
		if (handler)
		{
			handler.energy += energyBonus;
			return true;
		}
		else return false;
	}
}
