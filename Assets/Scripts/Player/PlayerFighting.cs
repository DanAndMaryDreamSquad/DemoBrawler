using UnityEngine;
using System.Collections;

public class PlayerFighting : MonoBehaviour {
	
	public Animator animator;
	public GameObject hitbox;
	public bool IsComboing {get; set;}
	public bool IsAcceptingComboInput {get; set;}
	public bool IsQueuedPunch {get; set;}

	// Use this for initialization
	void Start () {
		IsComboing = false;
		IsAcceptingComboInput = false;
		IsQueuedPunch = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!IsComboing && Input.GetButtonDown("Fire1")) {
			animator.SetTrigger("Punch1");
			IsComboing = true;
		}
		if (IsAcceptingComboInput && Input.GetButtonDown("Fire1")) {
			Debug.Log("queued punch");
			IsQueuedPunch = true;
		}
	}

	public void StartCombo() {

	}

	public void DoneWindingUp() {
		hitbox.SetActive(true);
	}

	public void DonePunching() {
		if (IsQueuedPunch) {
			animator.SetTrigger("Punch2");
			IsQueuedPunch = false;
		} else {
			hitbox.SetActive(false);
			IsComboing = false;
			IsAcceptingComboInput = false;
		}
	}

	public void AcceptingComboInput() {
		IsAcceptingComboInput = true;
	}
}
