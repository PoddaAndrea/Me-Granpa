using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // libreria SceneManagment
using UnityEngine.UI; // libreria UI (user interface)


//carica la scena quando si entra in una porta e riproduce la transizione

public class LoadSceneOnEnter : MonoBehaviour {
	private Animator anim; //variabile di tipo animator (accede a tutte le funzioni della classe Animator)
	public Image img; //variabile di tipo imge (accede a tutte le funzioni della classe Image)
    public bool entrance; // controllo del "trigger" di entrata
	public string sceneToLoad;// nome della scena

	private bool managerFound = false;
	private GameObject gameManagerObject; //variabile di tipo GameObject (accede a tutte le funzioni della classe GameObject)
    private GameManager manager; // variabile di tipo gameManager ((accede a tutte le funzioni della classe GameManager)
    
    /*OnTriggerEnter viene chiamato quando l'altro Collider inserisce il trigger.
      Questo messaggio viene inviato al trigger Collider e al Rigidbody (se ce ne sono) a cui appartiene il Collider del trigger, 
      e al Rigidbody (o al Collider se non c'è Rigidbody) che tocca il grilletto. 
      
      Note: gli eventi di trigger vengono inviati solo se uno dei Collider ha anche un Rigidbody collegato. 
      Gli eventi trigger verranno inviati ai MonoBehaviours disabilitati, per consentire l'attivazione di comportamenti in risposta a collisioni. 
      OnTriggerEnter si verifica in FixUpdate dopo una collisione. 
      I Collider coinvolti non sono garantiti per essere al punto di contatto iniziale.
     */

    void OnTriggerEnter (Collider other) {

        //entra nel blocco se il collider Player entra nel trigger
		if (other.gameObject.CompareTag("Player"))
		{
			manager.SetInputEnable(false); // setta l'input a false
			anim = img.GetComponent<Animator>(); //getComponent: Restituisce il componente di tipo Animator 
                                                //               se l'oggetto del gioco ne ha uno collegato, null se non lo fa.


             /*Creare una coroutine in Unity vuol dire essenzialmente creare un iteratore. 
             * Quando si passa l'iteratore al metodo StartCoroutine(), il motore chiede il prossimo oggetto 
             * di quel metodo ad ogni frame successivo, finché il metodo termina. 
             * La parola chiave yield è quella che produce gli oggetti, 
             * tutto il resto è da considerare un effetto collaterale dell'iteratore. 
             * WaitForSeconds è usato per dare alla coroutine un comportamento controllato, 
             * ma in generale si comporta esattamente come iteratore.
             */

            StartCoroutine(Fade());// la coroutine (richiamabile dalla MonoBehaviour) evita rallentamenti dovuti alla gestione di tanti oggetti

            FindObjectOfType<GameManager>().GetComponent<GameManager>().SetEntry(entrance); 

		}

	}

	void FixedUpdate() { 
        //se vero entra nel blocco
		if (!managerFound) {
			gameManagerObject = GameObject.FindWithTag("GameManager"); // assegna a gameManagerObject il gameObject con quel determinato tag
			manager = gameManagerObject.GetComponent<GameManager>();//getComponent: Restituisce il componente di tipo GameManger 
                                                                    //               se l'oggetto del gioco ne ha uno collegato, null se non lo fa.

            managerFound = true; // assegna true a managerFound
		}
	}


	IEnumerator Fade()
	{
		anim.SetBool("Fade", true); //attivare le transizioni tra gli stati degli animatori. nel nostro caso Fade
        yield return new WaitUntil(() => img.color.a == 1); //La parola chiave yield è quella che produce gli oggetti legata alla coroutine

        manager.SetInputEnable(true); //assegna il valore true a manager.setInputEnable
		SceneManager.LoadScene(sceneToLoad); // carica la scena

	}
}
