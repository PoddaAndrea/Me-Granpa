using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//questa classe permette al fuoco dell'hub centrale di accendersi una volta completato il dungeon

public class LampController : MonoBehaviour {
	public ParticleSystem fire; //variabile di tipo particellare (ParticleSystem)
	private GameObject gameManagerObject; // il nostro gameobject
	private GameManager manager; // variabile di tipo GameManager ci permette di accedere alle funzioni di questa determinata classe
	private bool ignited = false; // controllo
	private bool found = false; // controllo


	void FixedUpdate () {
        //se vero entra nel primo blocco
		if (!found) {
            //gameManagerObject prende lo stesso gemeObject del tag corrispondente.
			gameManagerObject = GameObject.FindWithTag("GameManager"); //Findwithtag: restituisce un gameobject con quel determinato tag altrimenti restituisce null
			manager = gameManagerObject.GetComponent<GameManager>(); //getComponent: Restituisce il componente di tipo GameManager 
                                                                    //               se l'oggetto del gioco ne ha uno collegato, null se non lo fa.
        }

            //se entrambe le condizioni sono entrambe vere entra nel primo blocco
            if (manager.GetFire() && !ignited) {
			fire.Play();//si attivano le particelle all'interno del gioco
			ignited = true; //assegna true alla variabile ignited
		}
	}
}
