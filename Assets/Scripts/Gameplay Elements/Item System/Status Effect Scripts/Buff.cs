using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
	//Should return true if the buff could be applied to the recieving object
	public abstract bool applyEffect(Buffable target);
}
