using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

//carica la scena e riproduce la transizione

public class LoadScene : MonoBehaviour {
	private Animator anim;
	public Image img;

	public void LoadByIndex(int sceneIndex)
	{
		anim = img.GetComponent<Animator>();
		FindObjectOfType<GameManager>().GetComponent<GameManager>().SetEntry(true);
		StartCoroutine(Fade(sceneIndex));


	}

	IEnumerator Fade(int sceneIndex) {
		anim.SetBool("Fade", true);
		yield return new WaitUntil(() => img.color.a == 1);

		SceneManager.LoadScene (sceneIndex);


	}


}
