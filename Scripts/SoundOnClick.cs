using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnClick : MonoBehaviour {
	public AudioClip buttonPressSound;
	private AudioSource source;

	void Start() {
		source = GetComponent<AudioSource>();	
	}

	void PlaySound() {
		source.PlayOneShot(buttonPressSound);
	}
}
