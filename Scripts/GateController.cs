using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Questa classe permette il controllo del gate nell'hub centrale
public class GateController : MonoBehaviour {
	private GameObject gameManagerObject;
	private GameManager manager;
	private bool managerFound = false;


	void FixedUpdate()
	{
        //se managerFound è true entra nel blocco
        if (!managerFound)
		{
			gameManagerObject = GameObject.FindWithTag("GameManager"); //cerca il gameManager e lo assegna a gameManagerObject
			manager = gameManagerObject.GetComponent<GameManager>();// assegna a manger i "componenti" di Gamemanager (accede alle funzioni della classe)
			managerFound = true; //assegna true a managerFound
		}

        //se managerFound è true entra nel blocco
        if (manager.isFireOn) {
			gameObject.SetActive(false);
		}

	}
	
}
