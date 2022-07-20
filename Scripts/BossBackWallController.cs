using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBackWallController : MonoBehaviour {
	public GameObject wall;
	public GameObject boss;
	public AudioSource music;
	private bool managerFound = false;
	private GameManager manager;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!managerFound) {
			//gameManagerObject = GameObject.FindWithTag("GameManager");
			manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
			managerFound = true;
		}
		if (manager.GetPlayerLife() <= 0) {
			music.Stop();
		}
	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			music.Play();
			boss.SetActive(true);
			wall.SetActive(true);
		}
	}


}
