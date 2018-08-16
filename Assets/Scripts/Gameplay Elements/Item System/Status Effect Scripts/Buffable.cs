using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Buffable : MonoBehaviour
{
	/*public struct appliedEffect
	{
		public appliedEffect(StatusEffect effect)
		{
			tick = effect.tick;
			revert = effect.removeEffect;
		}

		public delegate void effectMethod(Buffable target);

		public effectMethod revert;
		public effectMethod tick;
	}

	List<appliedEffect> appliedEffects = new List<appliedEffect>();
	List<float> effectTimers = new List<float>();*/

	public List<StatusEffect> effects = new List<StatusEffect>();

	public bool addStatusEffect(StatusEffect effect)
	{
		//Attempt to apply the effect parameter
		if (effect.applyEffect(this))
		{
			//If successful, create an instance of appliedEffect and add it to the list of effect timers
			Debug.Log(name + " Adding " + effect.name);
			effects.Add(ObjectExtensions.Copy(effect));
			//appliedEffects.Add(new appliedEffect(effect));
			//effectTimers.Add(effect.duration);
			return true;
		}
		else return false;
	}

	public void removeStatusEffect(StatusEffect effect)
	{
		effect.removeEffect(this);
		effects.Remove(effect);
	}

	protected virtual void Update()
	{
		StatusEffect[] MFD = new StatusEffect[effects.Count];
		int i = 0;
		foreach (StatusEffect effect in effects)
		{
			effect.tick(this);
			Debug.Log(name + " " + effect.duration);
			if(effect.duration <= 0)
			{
				MFD[i] = effect;
				i++;
			}
		}

		for (int j=0;j< i; j++)
		{
			removeStatusEffect(MFD[j]);
		}


	}

	/*public void removeStatusEffect(appliedEffect effect)
	{
		Debug.Log(name + " Removing " + effect);

		//Run the effect reversal code, and remove the effect and its timer from their respective lists

		int index = appliedEffects.IndexOf(effect);
		effect.revert(this);
		effectTimers.Remove(index);
		appliedEffects.Remove(effect);
	}*/

	/*protected virtual void Update()
	{
		int i = 0;
		foreach(appliedEffect effect in appliedEffects)
		{
			effect.tick(this);
			Debug.Log(name + " " + effectTimers[i]);

			effectTimers[i] -= Time.deltaTime;

			if (effectTimers[i] <= 0)
			{
				removeStatusEffect(effect);
			}
			i++;
		}
	}*/
}
