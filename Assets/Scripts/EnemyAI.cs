using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

	public float power = 1.0f;
	public Unit target;
	public Transform destination;

	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent>();
		this.agent.SetDestination (destination.position);
	}

	// Update is called once per frame
	void Update () {
		// Attack when it has reached the destination
		if (this.transform.position.x == destination.position.x & this.transform.position.z == destination.position.z) {
			if (target != null) {
				AttackBase ();
			}
		}
	}

	// Attack the target
	void AttackBase() {
		target.ReduceHP(this.power);	
	}
}
