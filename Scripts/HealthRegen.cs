using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Questa classe permette di rigenerare la vita la player
public class HealthRegen : MonoBehaviour {
	private bool managerFound = false;
	private GameManager manager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //se true accede al blocco
		if (!managerFound) {
			//gameManagerObject = GameObject.FindWithTag("GameManager");
			manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>(); //assegna a manager il gameObject contrassegnato con quel determinato tag
			managerFound = true; //assegna true a managerFound
		}

	}

    //questa funzione permette di aumentare al max la vita la player
	public void Regen() {
		manager.ResetPlayerLife();//funzione classe GameManager
	}
}
