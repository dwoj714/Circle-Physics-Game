using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DropPool : MonoBehaviour
{
	//Holds a list of PhysCircles to return, along with the chance to return each one
	//AAAnd you can't edit this in the inspector...
	//public Dictionary<PhysCircle, float> manifest;

	//Use these until you find something more elegant...  :(
	public PhysCircle[] circles;
	public float[] chances;

	/*void Start()
	{
		for(int i = 0; i < circles.Length; i++)
		{
			manifest.Add(circles[i], chances[i]);
		}
	}*/

	public PhysCircle getCircle()
	{
		float chanceTotal = 0;

		//Add up the chance associated with each PhysCircle
		foreach(float chance in chances)
		{
			chanceTotal += chance;
		}
		
		/*foreach(KeyValuePair<PhysCircle, float> pair in manifest)
		{
			chanceTotal += pair.Value;
		}*/

		//Generate a random value between 0 and chance total
		float selection = Random.value * chanceTotal;
		chanceTotal = 0;

		//Pretty sure the logic here results in unbiased randomness
		//With each iteration, each PhysCircle/float pair has [float] chance to surpass selection as chanceTotal increases
		for(int i = 0; i < chances.Length; i++)
		{
			chanceTotal += chances[i];
			if(chanceTotal > selection)
			{
				return circles[i];
			}
		}

		/*foreach(KeyValuePair<PhysCircle, float> pair in manifest)
		{
			chanceTotal += pair.Value;
			if (chanceTotal > selection)
			{
				return pair.Key;
			}
		}*/
		Debug.Log("RandomCircle returned null!!!");
		return null;
	}

	//Use getCircle to get a random PhysCircle, instantiate a clone of it (If it exists) and return a reference to it
	public PhysCircle spawnRandom(Transform parent)
	{
		PhysCircle newCircle = getCircle();

		if (newCircle)
		{
			return Instantiate(newCircle, parent.position, Quaternion.identity);
		}
		else return null;
	}

}
