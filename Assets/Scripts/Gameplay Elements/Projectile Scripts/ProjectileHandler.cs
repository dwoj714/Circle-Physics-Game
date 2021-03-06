﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : Buffable
{
	//The list of projectiles available to the handler
	public List<Projectile> projectiles = new List<Projectile>();

	[HideInInspector]
	public int projectileSelection = 0;
	[HideInInspector]
	public float[] cooldowns;

	//Displays projectile and indicates active projectile
	public WeaponBar weaponBar;

	//The amount of movement given when aiming the loaded projectile
	public float aimRadius = 1;

	Vector2 direction;
	float power;

	[HideInInspector]
	public bool loaded;

	//The PhysCirlce in control of this ProjectileHandler
	PhysCircle circle;

	//Reference to the loaded projectile
	//Replace this with an AmmoType, whenever that's implemented...
	Projectile shot;

	[HideInInspector]
	public bool swapped = false;
	private bool swapHolder = false;

	//If the magnitude of direction is less than this, don't fire
	public float deadZone = .05f;

	[HideInInspector]
	public float energy;
	public float maxEnergy = 100;
	public float rechargeSpeed = 20;

	void Start()
	{
		circle = GetComponent<PhysCircle>();
		energy = maxEnergy;
	}

	protected override void Update()
	{
		swapped = swapHolder;
		swapHolder = false;

		if(energy < maxEnergy)
		{
			energy += rechargeSpeed * Time.deltaTime;
		}
		else if(energy > maxEnergy)
		{
			energy = maxEnergy;
		}
		base.Update();
	}

	//instantiate a projectile, hold it at the center of the player
	public void ready(Projectile ammoType)
	{
		if (!loaded && ammoType.energyCost <= energy)
		{
			shot = GameObject.Instantiate(ammoType, transform.position, Quaternion.identity);
			shot.enabled = false;
			shot.rb.isKinematic = true;
			loaded = true;

			energy -= shot.energyCost;
		}
	}

	public void ready()
	{
		ready(projectiles[projectileSelection]);
	}

	public void aim(Vector2 direction, float power)
	{
		this.direction = direction.normalized;
		this.power = power;

		shot.transform.position = ((Vector2)transform.position + direction.normalized * power * -aimRadius);
	}

	public void fire()
	{
		if (power >= deadZone && shot)
		{
			shot.rb.isKinematic = false;

			float speedRange = shot.maxSpeed - shot.minSpeed;
			shot.speed = shot.minSpeed + speedRange * power;
			shot.rb.velocity = direction * shot.speed + circle.rb.velocity;
			shot.enabled = true;
		}
		else if(shot)
		{
			GameObject.Destroy(shot.gameObject);
		}
		loaded = false;
	}

	public void selectNext()
	{
		swapHolder = true;
		if(projectileSelection >= 0)
		{
			projectileSelection++;
		}
		if(projectileSelection >= projectiles.Count)
		{
			projectileSelection = 0;
		}
		weaponBar.hilightIndex(projectileSelection);
	}

	public void selectPrevious()
	{
		swapHolder = true;
		if (projectileSelection <= 0)
		{
			projectileSelection = projectiles.Count;
		}
		if (projectileSelection <= projectiles.Count)
		{
			projectileSelection--;
		}
		weaponBar.hilightIndex(projectileSelection);
	}

	public void select(int selection)
	{
		swapHolder = true;
		if(selection >= 0 && selection < projectiles.Count)
		{
			projectileSelection = selection;
		}
		weaponBar.hilightIndex(projectileSelection);
	}

	public Projectile activeProjectile()
	{
		return projectiles[projectileSelection];
	}

	//public void addBuff(item)

}