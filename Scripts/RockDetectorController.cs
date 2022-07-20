using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDetectorController : MonoBehaviour {
	public GameObject particles;
	public GameObject rock;

	private bool managerFound = false;
	private GameObject gameManagerObject;
	private GameManager manager;


	void FixedUpdate() { 
	if (!managerFound) {
			gameManagerObject = GameObject.FindWithTag("GameManager");
			manager = gameManagerObject.GetComponent<GameManager>();
			managerFound = true;
			if (manager.rightEntranceRockDestroyed) { 
				rock.SetActive(false);
			}
		}
	}


	void OnTriggerEnter(Collider other) {

		if (other.CompareTag("Granpa"))
		{
			FindObjectOfType<GameManager>().rightEntranceRockDestroyed = true;
			GameObject explosionParticles = Instantiate(particles, transform.position, Quaternion.identity);
			rock.SetActive(false);
		}
	}
}
