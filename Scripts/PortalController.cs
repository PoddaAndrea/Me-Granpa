using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalController : MonoBehaviour {

	private GameObject gameManagerObject;
	private GameManager manager;
	public string sceneToLoad;
	private bool managerFound = false;

	private Animator anim;
	public Image img;
	public bool entrance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!managerFound) {
			gameManagerObject = GameObject.FindWithTag("GameManager");
			manager = gameManagerObject.GetComponent<GameManager>();
			managerFound = true;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			//chatbox
			manager.SetInputEnable(false);
			manager.isFireOn = true;
			anim = img.GetComponent<Animator>();
            StartCoroutine(Fade());
		}
	}

	IEnumerator Fade()
	{
		anim.SetBool("Fade", true);
		yield return new WaitUntil(() => img.color.a == 1);
		manager.SetInputEnable(true);
		SceneManager.LoadScene(sceneToLoad);
	}
}
