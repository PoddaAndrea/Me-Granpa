using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//questa classe permette la chiusura del gioco
public class Exit : MonoBehaviour {
    //funzione che permette di uscre dal gioco
	public void Quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}
}
