using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //libreria user interface

public class VitaSystem : MonoBehaviour {
    public int vita; //la vita del PC presa dal game manager
    public int nCuori; // variabile che contiene il numero max di vite pubblico
    public Image[] cuori;//array di immagini
    public Sprite cuorePieno;// sprite cuore pieno
    public Sprite cuoreVuoto;//sprite cuore vuoto

    
	private bool managerFound = false; //controllo del managerfound
	private GameObject gameManagerObject; //variabile di tipo gameobject
	private GameManager manager;//variabile di tipo gamemanager

    
	
	void FixedUpdate () {

        //solito controllo per vedere se il gamemanager è stato trovato
		if (!managerFound) {
			gameManagerObject = GameObject.FindWithTag("GameManager");
			manager = gameManagerObject.GetComponent<GameManager>();
			managerFound = true;
			//Debug.Log("trovato");
		}

		vita = manager.GetPlayerLife(); //variabile vita prende il numero di vita max

        //scorre la "lista" di cuori e effettua i controlli
        for (int i = 0; i< cuori.Length; i++)
        {
            //primo controllo: la vita non può mai essere superiore al n Max di cuori se non ciò accade
            if (vita > nCuori)
            {
                vita = nCuori; //si assegnano i cuori di default
            }

            //assegna a cuori[i] lo sprite del cuore pieno o vuoto
            if (i < vita)
            {
                cuori[i].sprite = cuorePieno;
            }
            else {
                cuori[i].sprite = cuoreVuoto;//colpito
            }
            
            if (i < nCuori)
            {
                cuori[i].enabled = true;
            }
            else
            {
                cuori[i].enabled = false;//colpito
            }
        }
	}
}
