using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyVisualizer : Visualizer {

	ProjectileHandler source;

	protected override void Start()
	{
		base.Start();
		source = transform.parent.GetComponent<ProjectileHandler>();
	}

	// Update is called once per frame
	protected override void Update ()
	{
		base.Update();

		transform.localScale = new Vector3(source.energy / source.maxEnergy * initialScaleX, transform.localScale.y, transform.localScale.z);
	}
}
