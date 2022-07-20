using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	private bool opened = false;
	public GameObject dialogueManager;

	public void TriggerDialogue ()
	{
		
			if (!opened)
			{
				opened = true;
				dialogueManager.GetComponent<DialogueManager>().StartDialogue(dialogue);
				//FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

			}

			else
			{
				dialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
				//FindObjectOfType<DialogueManager>().DisplayNextSentence();
			}


	}

	public void ResetVariables() {
		opened = false;
	}
}
