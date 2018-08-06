using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBar : MonoBehaviour {

	[HideInInspector]
	public List<ProjectileIcon> iconList = new List<ProjectileIcon>();
	public ProjectileIcon icon;

	[HideInInspector]
	public ProjectileHandler weapon;

	new SpriteRenderer renderer;

	SpriteRenderer hilight;
	
	void Awake ()
	{
		renderer = GetComponent<SpriteRenderer>();
		hilight = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		weapon = GameObject.Find("Player").GetComponent<ProjectileHandler>();
		rebuildList();
	}

	public void hilightIndex(int index)
	{
		hilight.transform.position = iconList[index].transform.position;
	}

	public void rebuildList()
	{
		iconList.Clear();
		int i = 0;
		foreach (Projectile projectile in weapon.projectiles)
		{
			iconList.Add(GameObject.Instantiate(icon, transform));
			iconList[i].setSprite(projectile.GetComponent<SpriteRenderer>());
			float xScale = transform.localScale.x;
			iconList[i].transform.position += Vector3.right * (xScale/2 + i * xScale - weapon.projectiles.Count * xScale/2);
			i++;
		}
		renderer.size = Vector2.right * (2 + weapon.projectiles.Count) * 10 + Vector2.up * 10;
	}
}
