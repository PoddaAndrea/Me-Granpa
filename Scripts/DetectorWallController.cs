using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorWallController : MonoBehaviour
{
	public GameObject body;


	void OnTriggerEnter(Collider other)
	{
		
		if (other.CompareTag("Granpa"))
		{

			Destroy(body);
		}

	}
}
