using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : PhysCircle
{
	public Buff item;

	bool buffed = false;

	//On collision, attempt to apply the buff associated with this handler to any buffables that exist on the other collider.
	protected override void OnCollisionEnter2D(Collision2D hit)
	{
		foreach(Buffable target in hit.gameObject.GetComponents<Buffable>())
		{
			//Set buffed to true if any buffables can be affected by the item
			if(item is StatusEffect)
			{
				if (target.addStatusEffect((StatusEffect)item))
				{
					buffed = true;
				}
			}
			else
			{
				if (item.applyEffect(target))
				{
					buffed = true;
				}
			}

			if (buffed)
			{
				onItemAccept();
			}

		}
	}

	//What to do when the item is used. Default destroys the pickup.
	protected virtual void onItemAccept()
	{
		Destroy(this.gameObject);
	}
}
