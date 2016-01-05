using UnityEngine;
using System.Collections;

public class EnemyMover : MonoBehaviour {

	public float aggroDistance;
	public Animator animator;
	Transform player;
	//PlayerMover playerMover;
	NavMeshAgent nav;
	bool isWalking = false;
	bool isAggroed = false;

	void Awake() {
		GameObject p = GameObject.FindGameObjectWithTag("Player");
		player = p.transform;
		//playerMover = p.GetComponent<PlayerMover>();
		//animator = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update() {
		Debug.Log("Aggro: " + isAggroed);
		Debug.Log("Nav enabled: " + nav.enabled);
		CheckAggro();
		if (isAggroed) {
			nav.SetDestination(player.position);
			animator.SetBool("IsWalking", !ReachedDestination());
		} else {
			nav.enabled = false;
		}
	}

	void CheckAggro() {
		if (isAggroed) {
			return;
		}
		Vector3 distance = player.transform.position - this.transform.position;
		if (distance.sqrMagnitude < aggroDistance) {
			isAggroed = true;
			nav.enabled = true;
		}
	}

	bool ReachedDestination() {
		if (nav.pathStatus == NavMeshPathStatus.PathPartial) {
			return true;
		}
		if (!nav.pathPending){
			if (nav.remainingDistance <= nav.stoppingDistance){
				if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f){
					return true;
				}
			}
		}
		return false;
	}
}
