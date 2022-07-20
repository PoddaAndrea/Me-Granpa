using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingEscapeWallController : MonoBehaviour {
	private GameObject gameManagerObject;
	private GameManager manager;
	private bool found = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!found) {
			gameManagerObject = GameObject.FindWithTag("GameManager");
			manager = gameManagerObject.GetComponent<GameManager>();
		}


	}
}
