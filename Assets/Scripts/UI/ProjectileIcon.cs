using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ProjectileIcon : MonoBehaviour {

	new SpriteRenderer renderer;

	// Use this for initialization
	void Awake ()
	{
		renderer = GetComponent<SpriteRenderer>();
	}

	public void setSprite(SpriteRenderer targetRenderer, bool useScale = true)
	{
		renderer.sprite = targetRenderer.sprite;
		renderer.material = targetRenderer.sharedMaterial;
		renderer.color = targetRenderer.color;
		if (useScale)
		{
			transform.localScale = targetRenderer.transform.localScale * 10;
		}
	}
}
