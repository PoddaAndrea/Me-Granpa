using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactitesController : MonoBehaviour
{
	public GameObject sensorObject;
	private SenseController sensor;
	public GameObject rock;
	private Rigidbody rockRigidbody;


	void Start()
	{
		sensor = sensorObject.GetComponent<SenseController>();
		rockRigidbody = rock.GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		if (sensor.GetGranpa())
		{
			rockRigidbody.useGravity = true;
			sensor.SetGranpa(false);
		}
	}

}

