using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhereToLoad : MonoBehaviour {
	public GameObject position;

	// Carica il personaggio nella posizione di entrata se il game manager è impostato su entrata
	void Start () {
		if (!(FindObjectOfType<GameManager>().GetEntry())) {
			transform.position = position.transform.position;
		}
	}

}
