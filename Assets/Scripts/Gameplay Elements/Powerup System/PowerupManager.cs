using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
	static void buffExplosionRadius(ref ExplosiveProjectile projectile, float buff)
	{
		projectile.detonator.explosionRadius += buff;
	}
}
