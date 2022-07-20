using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedSign : MonoBehaviour {

	private bool managerFound = false;
	private GameObject gameManagerObject;
	private GameManager manager;

    //Questa classe permette di controllare la roccia a inizio gioco (se è stata distrutta o meno)

	void FixedUpdate()
	{
		if (!managerFound)
		{
			gameManagerObject = GameObject.FindWithTag("GameManager");
			manager = gameManagerObject.GetComponent<GameManager>();
			managerFound = true;
			if (manager.rightEntranceRockDestroyed)
			{
				gameObject.SetActive(false);
			}
		}
	}
}
