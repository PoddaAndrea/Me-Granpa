using UnityEngine;
using System.Collections;
using System;

public class CameraShake : MonoBehaviour
{

	public float linearIntensity = 0.25f;
	public float angularIntensity = 5f;
	public Vector3 oldPosition;
	public Quaternion oldRotation;
	[NonSerialized]
	public bool isShaking = false;

	private bool angularShaking = true;

	void Update()
	{
		if (isShaking)
		{
			oldPosition = transform.localPosition;
			oldRotation = transform.localRotation;

			//LinearShaking(); non lo uso più più bello effetto solo con angular shaking
			if (angularShaking)
				AngularShaking();
		}
	}

	private void LinearShaking()
	{
		Vector2 shake = UnityEngine.Random.insideUnitCircle * linearIntensity;
		Vector3 newPosition = transform.localPosition;
		newPosition.x = shake.x;
		newPosition.y = shake.y;
		transform.localPosition = newPosition;
	}

	private void AngularShaking()
	{
		float shake = UnityEngine.Random.Range(-angularIntensity, angularIntensity);
		transform.localRotation = Quaternion.Euler(0f, 0f, shake); //ruota la telecamera a random in un range prestabilito
	}

	public void SetAngularShaking(bool state)
	{
		angularShaking = state;
		if (!angularShaking)
			transform.localRotation = Quaternion.identity;
	}

	public void Enable()
	{
		isShaking = true;
	}

	public void Disable()
	{
		isShaking = false;
	}
}