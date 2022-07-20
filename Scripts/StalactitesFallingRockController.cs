using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StalactitesFallingRockController : MonoBehaviour {
	private GameObject rockObject;
	private Rigidbody RockBody;
	public ParticleSystem particlesExplosion;
	private Vector3 position;
	public AudioClip hitClip;
	private AudioSource audioS;

	// Use this for initialization
	void Start () {
		rockObject = GetComponent<GameObject>();
		RockBody = GetComponent<Rigidbody>();
		position = transform.localPosition;
		audioS= GetComponent<AudioSource>();
	}

	void OnCollisionEnter(Collision other) {
		Debug.Log(other.gameObject.tag);

			RockBody.useGravity = false;
			RockBody.isKinematic = true;
			audioS.PlayOneShot(hitClip);
			particlesExplosion.Play();
			transform.localPosition = position;

			RockBody.useGravity = false;
			RockBody.isKinematic = false;

	}
}
