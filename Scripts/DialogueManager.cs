using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//questa classe permette la gestione dei dialoghi all'interno del gioco
public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;
	public GameObject sign;
	public Animator animator;
	private GameManager manager;
	private Queue<string> sentences; //è una lista di stringhe in stile FIFO (First In First Out)

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>(); //creazione oggetto lista FIFO
		manager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

	}

    //funzione per far partire il dialogo (richiede come parametro un dialogo)
	public void StartDialogue (Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true); //attiva l'animazione della scritta di dialogo
		manager.SetInputEnable(false);//disattiva gli input del player
		nameText.text = dialogue.name;

		sentences.Clear();

        //scorre il dialogo della classe Dialougue
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence); //ultimo della lista
            
		}

		DisplayNextSentence(); // richiama la funzione prossimo dialogo
	}

    //funzione che permette di passare al dialogo successivo
	public void DisplayNextSentence ()
	{
        //se i dialoghi sono finiti richiama end Dialogue
        if (sentences.Count == 0)
		{
			EndDialogue();
			manager.SetInputEnable(true);
			return;
		}

		string sentence = sentences.Dequeue();// Dequeue la prima della lista
		StopAllCoroutines();//chiude la coroutine
		StartCoroutine(TypeSentence(sentence));//richiama la funzione iniziando la coroutine
	}

    //funzione che permette al dialogo di scorrere lettera per lettera

    /*Quando il ciclo foreach terminerà con il metodo TypeSentence, 
     * significherà che la frase completa è stata visualizzata nel testo 
     * della finestra di dialogo.
     */
        IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

    //funzione che termina il dialogo
	void EndDialogue()
	{	
		animator.SetBool("IsOpen", false);//disattiva l'animazione
		manager.SetInputEnable(true);//attiva gli input del player
		sign.GetComponent<DialogueTrigger>().ResetVariables();//resetta le variabili
	}

}
