using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InitialiseButton : MonoBehaviour
{
	GameObject lastselect;
	void Start()
	{
		lastselect = new GameObject();
	}
	// Update is called once per frame
	void Update()
	{ //se nessun bottone è selzionato nella GUI viene messo l'ultimo selezionato come selezionato
		if (EventSystem.current.currentSelectedGameObject == null)
		{
			EventSystem.current.SetSelectedGameObject(lastselect);
		}
		else
		{
			lastselect = EventSystem.current.currentSelectedGameObject;
		}
	}
}