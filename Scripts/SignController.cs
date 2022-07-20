using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//quando entri nella zona del cartello esso ti toglie la possibilità di saltare essendo lo stesso tasto per leggere, 
//ha due comportamenti diversi se è un cartello o il granpa, il sistema è lo stesso

public class SignController : MonoBehaviour {
	public Canvas sign;
	//public GameObject startText;
	private GameManager manager;
	public bool isGranpa = false;
	private bool played = false;
	public GameObject startText;


	void Start() {
		manager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player") && !isGranpa) {
			manager.SetJump(false);
			sign.gameObject.SetActive(true);
		}

		if (other.gameObject.CompareTag("Player") && isGranpa && !played) {
			manager.SetJump(false);
			manager.SetInputEnable(false);
			played = true;
			sign.gameObject.SetActive(true);

		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			manager.SetJump(true);
			sign.gameObject.SetActive(false);
		}	
	}


}
