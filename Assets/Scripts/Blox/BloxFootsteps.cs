using UnityEngine;
using System.Collections;

public class BloxFootsteps : MonoBehaviour {
	
	public AudioSource footstepAudio;
	private float lastFootstep = 0f;

	public void PlayFootstep() {
		if (lastFootstep + 0.12f < Time.time) {
			footstepAudio.Play();
			lastFootstep = Time.time;
		}
	}
}
