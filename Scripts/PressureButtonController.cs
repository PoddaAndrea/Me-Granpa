using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


//classe che permette il controllo dei bottoni. scena di gioco: cancelli rossi blu
public class PressureButtonController : MonoBehaviour
{
	public static bool redButtonState = false;
	public static bool blueButtonState = true;

	public bool isPlayerOn = false;

	public GameObject sensorObject;
	private SenseController sensor;

	private Animator buttonAnimator;

	void Start()
	{
		sensor = sensorObject.GetComponent<SenseController>();
		buttonAnimator = GetComponent<Animator>();
		redButtonState = false;
		blueButtonState = true;
		Debug.Log("bottone Rosso: " + redButtonState);
		Debug.Log("bottone Blu: " + blueButtonState);
	}



	public void ActivatedBlue() //attiva blu e disattiva rosso
	{
		Debug.Log("Attivato blu");
		redButtonState = true;
		blueButtonState = false;
		//riproduci animazione 
		buttonAnimator.SetBool("Pressed", true);
	}

	public void ActivatedRed()//attiva rosso e disattiva blu
	{
		Debug.Log("Attivato rosso");
		blueButtonState = true;
		redButtonState = false;
		buttonAnimator.SetBool("Pressed", true);
	}

	public bool GetRed()
	{
		return redButtonState;
	}

	public bool GetBlue()
	{
		return blueButtonState;
	}

	public void OnCollisionStay(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			isPlayerOn = true;

			if(redButtonState && isPlayerOn)
			{

				if (sensor.GetGranpa())//se ci sta sopra il giocatore e usa il nonno e rosso è attivo
				{
					Debug.Log(other.gameObject.tag + "ROSSO");
					sensor.SetGranpa(false);
					ActivatedRed();
				}
			}

			if(blueButtonState && isPlayerOn)//se ci sta sopra il giocatore e usa il nonno e blu è attivo
			{
				if (sensor.GetGranpa())
				{
					Debug.Log(other.gameObject.tag + "BLU");
					sensor.SetGranpa(false);
					ActivatedBlue();
				}
			}
		}
	}

	public void OnCollisionExit(Collision other) { 
		if (other.gameObject.CompareTag("Player")) { 
			//esci dall animazione e torna alla posizione di default
			buttonAnimator.SetBool("Pressed", false);
			isPlayerOn = false;
		}
	}

}