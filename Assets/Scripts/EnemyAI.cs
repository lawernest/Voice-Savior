using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

	public float power = 1.0f;
	public GameObject target;
	public Transform start;
	public Transform end;

	private NavMeshAgent agent;
	private bool isMoving = true;

	// Use this for initialization
	void Start () {
		this.transform.position = start.position;
		agent = this.GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update () {
		if (isMoving) {
			this.agent.SetDestination (end.position);
			if (this.transform.position.x == end.position.x & this.transform.position.z == end.position.z) {
				isMoving = false;
			}
		} else {
			if (target != null) {
				AttackBase ();
			}
		}
	}

	// Attack the target
	void AttackBase() {
		target.gameObject.GetComponent<Unit>().ReduceHP(this.power);	
	}
	void OnCollisionStay(Collision collide) {
		Debug.Log (collide.gameObject.name);
	}
}
