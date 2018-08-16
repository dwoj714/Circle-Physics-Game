using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : Buff
{
	public float duration = 5;

	public abstract void removeEffect(Buffable target);

	public virtual void tick(Buffable target)
	{
		duration -= Time.deltaTime;
		//Debug.Log(this.GetType());
	}

}