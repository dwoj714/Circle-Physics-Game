using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RelativeJoint2D))]
public class StickyBomb : ExplosiveProjectile
{
	RelativeJoint2D joint;

	protected override void Awake()
	{
		base.Awake();
		joint = GetComponent<RelativeJoint2D>();
	}

	void Start()
	{
		joint.enabled = false;
		joint.connectedBody = GameObject.Find("World Rigidbody").GetComponent<Rigidbody2D>();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		//Debug.Log(joint.connectedBody);

		//disable fixed speed when connected to another rigidbody
		if (joint.enabled)
		{
			hasFixedSpeed = false;
		}
		else
		{
			hasFixedSpeed = true;
		}
	}

	protected override void OnCollisionEnter2D(Collision2D hit)
	{
		ColliderDistance2D cd = col.Distance(hit.collider);

		Rigidbody2D hitRb = hit.collider.GetComponent<Rigidbody2D>();
		if (hitRb)
		{
			joint.connectedBody = hitRb;
		}

		joint.enabled = true;
		detonator.sparked = true;
	}

	void OnExplosion()
	{
		GameObject.Destroy(this.gameObject);
	}

}
