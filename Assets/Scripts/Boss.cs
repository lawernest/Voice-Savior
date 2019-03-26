using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Unit {

	protected override void Start () {
		base.Start();
	}

	public override void Initialize(float hp, float damage, int cost) {
		base.Initialize(hp * 4, damage * 2, cost);
	}
		
	protected override void Update () {
		base.Update();
	}
}
