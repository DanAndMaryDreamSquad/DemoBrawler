using UnityEngine;
using System.Collections;

public class BloxAnimations : MonoBehaviour {

	public PlayerFighting playerFighting;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void DoneWindingUp() {
		Debug.Log("done winding up");
		playerFighting.DoneWindingUp();
	}

	void DonePunching() {
		Debug.Log("done punching");
		playerFighting.DonePunching();
	}

	void StartCombo() {
		Debug.Log("starting combo");
		playerFighting.StartCombo();
	}

	void AcceptingComboInput() {
		Debug.Log("accepting input");
		playerFighting.AcceptingComboInput();
	}


}
