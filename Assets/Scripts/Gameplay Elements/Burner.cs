using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour {

	public float DPS;

	void OnTriggerStay2D(Collider2D col)
	{
		HealthBar hb = col.GetComponent<HealthBar>();
		if(hb)
		{
			hb.takeDamage(DPS * Time.deltaTime);
		}
	}
}
