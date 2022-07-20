using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorController : MonoBehaviour
{
	public GameObject body;


	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			
				body.GetComponent<EnemyController>().challenged = true;

				
		}

		if (other.CompareTag("Granpa"))
		{
			
				body.GetComponent<EnemyController>().stun = true;
		}

	}
}
