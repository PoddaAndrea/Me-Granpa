using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRocksController : MonoBehaviour {

	public GameObject sensor;
	public ParticleSystem particles;

	private SenseController sensorController;

	private bool isGrounded = false;

	// Use this for initialization
	void Start () {
		sensorController = sensor.GetComponent<SenseController>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		
		if (sensorController.GetGranpa() && isGrounded) {
			//sensorController.SetGranpa(false);
			//Debug.Log("granpa " + sensorController.GetGranpa() + " is grounded " + isGrounded);
			particles.Play();
			Destroy(gameObject);
		}
		if (sensorController.GetGranpa() && !isGrounded) { 
			sensorController.SetGranpa(false);
		}
		sensorController.SetGranpa(false);



	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{
			
            isGrounded = true;
			//StartCoroutine(WaitForGrounded());


		}
	}

	/*IEnumerator WaitForGrounded()
	{

		yield return new WaitForSecondsRealtime(1);

		isGrounded = true;
	}*/
}
