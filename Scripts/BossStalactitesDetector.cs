using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStalactitesDetector : MonoBehaviour {
	public GameObject bossObject;
	private BossController boss;
	// Use this for initialization
	void Start () {
		boss = bossObject.GetComponent<BossController>();
	}

	void OntriggerEnter(Collider other)
	{
		Debug.Log(other.tag);
		if (other.gameObject.CompareTag("Stalactites"))
		{
			boss.life --;
		}
	}
}
