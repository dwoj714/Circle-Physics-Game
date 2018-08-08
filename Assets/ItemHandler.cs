using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : PhysCircle
{
	public Item item;

	protected override void OnCollisionEnter2D(Collision2D hit)
	{
		if (item.applyEffect(hit.gameObject))
			Destroy(this.gameObject);
	}
}
