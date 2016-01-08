using UnityEngine;
using System.Collections;

public class PlayerFighting : MonoBehaviour {
	
	public Animator animator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire1")) {
			Debug.Log("punching");
			animator.SetTrigger("Punch");
		}
	
	}
}
