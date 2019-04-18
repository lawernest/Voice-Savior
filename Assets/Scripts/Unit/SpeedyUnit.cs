using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyUnit : Unit {

	protected override void Start () {
		base.Start();
	}

	public override void Initialize(float hp, float damage, int cost) {
		base.Initialize(hp * 0.6f, damage, cost);
	}

	protected override void Update () {
		base.Update();
	}
}
