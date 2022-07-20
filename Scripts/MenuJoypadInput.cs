using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//script per controllare il menu da joypad

public class MenuJoypadInput : MonoBehaviour
{
	private Button previousButton;

	public float scaleAmount = 1f;
	public GameObject defaultButton;

	void Start()
	{
		if (defaultButton != null)
		{
			EventSystem.current.SetSelectedGameObject(defaultButton);
		}
	}
	void Update()
	{
		
		var selectedObj = EventSystem.current.currentSelectedGameObject;

		if (selectedObj == null) return;

		var selectedAsButton = selectedObj.GetComponent<Button>();

		if (selectedAsButton != null && selectedAsButton != previousButton)
		{
			if (selectedAsButton.transform.name != "PauseButton")
				HighlightButton(selectedAsButton);
		}

		if (previousButton != null && previousButton != selectedAsButton)
		{
			UnHighlightButton(previousButton);
		}
		previousButton = selectedAsButton;
	}
	void OnDisable()
	{
		if (previousButton != null) UnHighlightButton(previousButton);
	}

	void HighlightButton(Button butt)
	{
		//if (SettingsManager.Instance.UsingTouchControls) return;
		butt.transform.localScale = new Vector3(scaleAmount, scaleAmount, scaleAmount);
	}

	void UnHighlightButton(Button butt)
	{
		//if (SettingsManager.Instance.UsingTouchControls) return;
		butt.transform.localScale = new Vector3(1, 1, 1);
	}

	public void ReloadDefaults() { //rimette il bottone di default come selezionato, usato quando si passa a un altro menu e si vuole tornare a questo
		EventSystem.current.SetSelectedGameObject(defaultButton);
	}
}