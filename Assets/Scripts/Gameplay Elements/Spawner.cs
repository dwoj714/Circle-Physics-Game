using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float frequency;

    public List<BombController> bombList;

    private float clock;

	public Vector2 initialVelocity;

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

		pos.x = transform.position.x + (Random.value -0.5f) * transform.localScale.x;

		BombController newBomb = Instantiate(getRandomBomb(), pos, Quaternion.identity).GetComponent<BombController>();

		newBomb.rb.velocity = initialVelocity;
	}

	BombController getRandomBomb()
	{
		float total = 0;

		//Calculate the total of the chance variables associated with each bomb type in the list
		foreach(BombController bomb in bombList)
		{
			total += bomb.spawnChance;
		}

		//Generate a random number between 0 and the total
		float selection = Random.value * total;
		total = 0;

		//Iterate through the list of bomb
		foreach (BombController bomb in bombList)
		{
			total += bomb.spawnChance;
			if (total > selection)
				return bomb;
		}
		return null;
	}
}
