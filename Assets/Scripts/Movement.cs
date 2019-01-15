using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour {

	public Transform start;
	public Transform end;

	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		this.transform.position = start.position;
		agent = this.GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update () {
		//Move to the target position
		this.agent.SetDestination(end.position);
		if (this.transform.position.x == end.position.x & this.transform.position.z == end.position.z) {
			Destroy(this.gameObject);
		}
	}
}