using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*questa classe permette all NPC di seguire il player principale ad una velocita e distanza prestabilita*/
public class GranpaController : MonoBehaviour {
	public GameObject player; //il player
	public float speed; //la velocita
	public float offset; // la distanza prestabilita
	private Vector3 directionOfCharacter; // direzione Npc

    /*FixedUpdate è una funzione che Unity chiama in automatico ad intervalli
regolari che non tengono conto del frame rate (di default FixedUpdate viene chiamato
ogni 0.02 secondi).
FixedUpdate viene chiamato subito prima di fare i calcoli relativi al motore fisico, e per
questo motivo viene utilizzato principalmente per effettuare operazioni che riguardano la
fisica*/

	void FixedUpdate () {

		transform.LookAt(player.transform); //permette di ruotare la posizione

        //vector3.distance ritorna la distanza tra a e b
        // se la distanza tra a e b è maggiore o uguale alla dimensione prestabilita
		if ( Vector3.Distance(player.transform.position, transform.position) >= offset)
		{
			directionOfCharacter = player.transform.position - transform.position;
			directionOfCharacter = directionOfCharacter.normalized;    // Get Direction to Move toword il vettore prende la stessa direzione ma la sua lunghezza è 1.0
            transform.position = new Vector3(transform.position.x, player.transform.position.y + 2.3f, transform.position.z);//aggiorna la posizione corrente del player
			transform.Translate(directionOfCharacter* speed /* * time.deltaTime*/ , Space.World); //trasforma la direzione
            /*sarebbe stato meglio utilizzare il time.deltaTime per stabilizzare gli fps*/
		}

        /*NB: fare il tranform all'interno del fixedUpdate è un inutile spreco di risorse  il
              risultato sarebbe che verrebbero eseguite più volte (nelFixedUpdate) prima che la scena
              possa essere renderizzata, ma senza che il giocatore veda effettivamente alcun
              cambiamento.
        */
    }
}
