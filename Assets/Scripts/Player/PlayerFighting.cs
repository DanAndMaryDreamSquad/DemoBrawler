using UnityEngine;
using System.Collections;

public class PlayerFighting : MonoBehaviour {
	
	public Animator animator;
	public GameObject hitbox;
	public bool IsComboing {get; set;}

	// Use this for initialization
	void Start () {
		IsComboing = false;	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire1")) {
			animator.SetTrigger("Punch1");
			IsComboing = true;
		}	
	}

	public void DoneWindingUp() {
		hitbox.SetActive(true);
	}

	public void DonePunching() {
		hitbox.SetActive(false);
		IsComboing = false;
	}
}
