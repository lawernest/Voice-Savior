using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

	private Unit target;
	private Transform destination;
	private NavMeshAgent agent;
	private Damager damager;

	// Use this for initialization
	void Start() {
		this.agent = this.GetComponent<NavMeshAgent>();
		this.damager = this.GetComponent<Damager>();
		this.agent.SetDestination(destination.position);
	}

	public void Init(Transform destination, Unit target) {
		this.destination = destination;
		this.target = target;
	}

	// Update is called once per frame
	void Update () {
		// Attack when it has reached the destination
		if (this.transform.position.x == destination.position.x & this.transform.position.z == destination.position.z) {
			if (target != null) {
				this.damager.Attack(this.target);
			}
		}
	}
}