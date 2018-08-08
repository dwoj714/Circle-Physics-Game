using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyItem : Item
{

	public float energyBonus = 50;

	public override bool applyEffect(GameObject obj)
	{
		this.obj = obj;

		//Attempt to get a reference to the ProjectileHandler of the object we're giving the energy to
		ProjectileHandler handler = obj.GetComponent<ProjectileHandler>();

		//If handler exists, add to it's energy pool
		if (handler)
		{
			handler.energy += energyBonus;
			return true;
		}
		else return false;
	}

	//One time buff, removal not necessary
	public override void removeEffect()
	{

	}

}
