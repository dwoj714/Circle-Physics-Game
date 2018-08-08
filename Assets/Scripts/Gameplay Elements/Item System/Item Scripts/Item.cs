using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
	protected GameObject obj;
	//Should return true if the buff could be applied to the reciever object
	public abstract bool applyEffect(GameObject obj);
	public abstract void removeEffect();
}
