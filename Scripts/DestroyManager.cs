using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Questa classe ci permette di eliminare il gameManager mediante l'apposita  funzione
public class DestroyManager : MonoBehaviour {

	public void DestroyGameManager() { 
		GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestroyManager();
	}
}
