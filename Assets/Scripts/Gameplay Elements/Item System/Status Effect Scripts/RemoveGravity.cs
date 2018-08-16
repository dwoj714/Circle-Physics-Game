using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGravity : StatusEffect
{
	BombController buffTarget;
	float storedGravity;
	bool applied = false;

	public override bool applyEffect(Buffable target)
	{
		//The target of this effect is a BombController
		buffTarget = target.GetComponent<BombController>();

		//Set the BombController's (not RigidBody2D) gravity to 0
		//Return false if the given object doesn't have a BombController
		if (buffTarget)
		{
			storedGravity = buffTarget.gravity;
			buffTarget.gravity = 0;
			applied = true;
			return true;
		}
		else return false;
	}

	//Revert the gravity to what it was
	public override void removeEffect(Buffable target)
	{
		BombController controller = (BombController)target;
		controller.gravity = storedGravity;
	}
}
