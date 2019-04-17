using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUnit : Unit {

	protected override void Start () {
		base.Start();
	}

	public override void Initialize(float hp, float damage, int cost) {
		base.Initialize(hp * 1.5f, damage * 2.0f, cost);
	}

	protected override void Update () {
		base.Update();
	}
}
