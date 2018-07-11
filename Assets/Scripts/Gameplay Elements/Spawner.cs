using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float frequency;

	private float clock;
	private float range;

	public Transform Bomb;

	public Vector2 initialVelocity;

	// Use this for initialization
	void Start ()
	{
		range = transform.localScale.x - Bomb.GetComponent<CircleCollider2D>().radius;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		clock -= Time.deltaTime;

		if(clock < 0)
		{
			onClockTimeout();
			clock = frequency;
		}

	}

	void onClockTimeout()
	{
		Vector3 pos = transform.position;

		pos.x = transform.position.x + (Random.value -0.5f) * range;

		BombController newBomb = Instantiate(Bomb, pos, Quaternion.identity).GetComponent<BombController>();

		newBomb.rb.velocity = initialVelocity;
	}

}
