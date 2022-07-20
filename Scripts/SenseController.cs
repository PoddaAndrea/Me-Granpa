using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sensore universale per gli NPC

public class SenseController : MonoBehaviour {

	private bool granpa = false; 
	private bool Player = false;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Granpa"))
		{
			//granpa = true;
			Debug.Log(other.tag);
			StartCoroutine(WaitForGranpaDisappereance());

		}	

		if (other.CompareTag("Player"))
		{
			Player = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Player = false;
		}
	}

	IEnumerator WaitForGranpaDisappereance() { //rimette l'avvistamento del granpa a false dopo poco tempo, ossia quando scompare
		granpa = true;
		yield return new WaitForSecondsRealtime(0.5f);
		granpa = false;
	}

	public bool GetGranpa() {
		return granpa;
	}

	public void SetGranpa(bool state) {
		granpa = state;
	}

	public bool GetPlayer() {
		return Player;
	}


}
