using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PhysCircle : MonoBehaviour {

	[HideInInspector]
	public CircleCollider2D col;

	[HideInInspector]
	public Rigidbody2D rb;

	private HealthBar hb;

	protected virtual void Awake()
	{
		col = GetComponent<CircleCollider2D>();
		rb = GetComponent<Rigidbody2D>();
		hb = GetComponent<HealthBar>();
	}

	//Returns the radius of the collider adjusted by how it's scaled
	public float radius()
	{
		float greaterScale = transform.lossyScale.x > transform.lossyScale.y ? transform.lossyScale.z : transform.lossyScale.y;
		return col.radius * greaterScale;
	}

	protected virtual void OnCollisionEnter2D(Collision2D hit)
	{

	}

}
