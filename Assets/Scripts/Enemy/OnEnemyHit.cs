using UnityEngine;
using System.Collections;

public class OnEnemyHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("PlayerAttack")) {
			Debug.Log("Enemy got hit");
		}
	}
}
