using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Buffable
{
	void addBuff(Item item);
	void removeBuff(Item item);
}
