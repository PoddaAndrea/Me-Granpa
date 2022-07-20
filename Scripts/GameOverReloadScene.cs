using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverReloadScene : MonoBehaviour {
private bool managerFound = false;
private GameObject gameManagerObject;
private GameManager manager;
	private string sceneToLoad;

	public void ReloadScene ()
    {

		if (!managerFound) {
			gameManagerObject = GameObject.FindWithTag("GameManager");
			manager = gameManagerObject.GetComponent<GameManager>();
			managerFound = true;

		}
		manager.SetInputEnable(true);
		manager.SetPlayerLife(manager.playerLifeAtStart);
		sceneToLoad = SceneManager.GetActiveScene().name;
		//SceneManager.UnloadScene(SceneManager.GetActiveScene());
		SceneManager.LoadScene(sceneToLoad);

		Time.timeScale = 1f;
        Debug.Log("Ricarica la scena");
        
    }
}
