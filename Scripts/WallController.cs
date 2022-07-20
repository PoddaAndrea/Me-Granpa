using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

//animatore dei muri colorati

public class WallController : MonoBehaviour {
	public GameObject pressureButtonObject;
	private PressureButtonController pressureButton;

	public bool red;
	public bool blue;

	private Animator wallAnimator;

	void Start() {
		pressureButton = pressureButtonObject.GetComponent<PressureButtonController>();
		wallAnimator = GetComponent<Animator>();
	}

	void FixedUpdate () {
		


		//se rosso è attivato e il muro è rosso OPPURE blu è attivato e il muro e blu alzo QUESTO muro
		if ((pressureButton.GetRed() && red) || (pressureButton.GetBlue() && blue)) {
			//riproduci one time animation e stai fermo
			wallAnimator.SetBool("Activated" , true);

		}
		//se rosso è attivato e il muro è blu OPPURE blu è attivato e il muro è rosso abbasso QUESTO muro
		if ((pressureButton.GetRed() && blue) || (pressureButton.GetBlue() && red)) { 
			wallAnimator.SetBool("Activated", false);

		}

	}
}
