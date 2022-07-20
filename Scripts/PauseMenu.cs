using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //libreria SceneManagement

//Questa classe permette il controllo generale del menu di pausa e di tutte le sue funzioni principali
public class PauseMenu: MonoBehaviour
{
	private bool managerFound = false;
	private GameObject gameManagerObject;
	private GameManager manager;

    public static bool InPausa = false;
    public GameObject MenuPausaUI;
    public GameObject RichiestaExitUi;


    
    void Update()
    {
        //solito controllo del gameManager
        if (!managerFound) {
			gameManagerObject = GameObject.FindWithTag("GameManager");
			manager = gameManagerObject.GetComponent<GameManager>();
			managerFound = true;
		}
        //se viene premuto il tasto esc (se il valore di ritorno è true) accedi al blocco successivo
		if (Input.GetButtonDown("Escape"))
        {
            if (InPausa) // se il volore di ritorno è true (è in pausa) accedi al blocco Riprendi() altrimenti accedi al blocco Pausa()
            {

                Riprendi(); //richiama la funzione riprendi
            }
            else
            {
                Pausa(); //richiama la funzione pausa
            }
        }
    }

    //funzione che permette di far riprende il gioco mediante la presione di un tasto prestabilito
    public void Riprendi()
    {
		MenuPausaUI.GetComponent<MenuJoypadInput>().ReloadDefaults();
		MenuPausaUI.SetActive(false);//disattiva il canvas del menu
        RichiestaExitUi.SetActive(false);//disattiva il canvas del richiesta di uscita
        Time.timeScale = 1f;// Time.timeScale: quando è impostato a 1 rappresenta il tempo "normale" di gioco
        InPausa = false;//disabilita Inpausa
		manager.SetInputEnable(true);
    }

    //funzione che permette di mettere in pausa il gioco tramite la pressione di un tasto prestabilito
    public void Pausa()
    {
		MenuPausaUI.GetComponent<MenuJoypadInput>().ReloadDefaults();//richiama i comandi da joypad
		manager.SetInputEnable(false);
        MenuPausaUI.SetActive(true);//attiva il canvas
        Time.timeScale = 0f;// Time.timeScale: Quando è impostato su zero, 
                           //il gioco viene sostanzialmente messo in pausa

        InPausa = true; //attiva inPause
    }

    //funzione che permette di caricare il menu principale mediante la pressione di un bottone
    public void CaricaMenu()
    {
        Time.timeScale = 1f; //impostare il tempo di gioco a "normale"

		manager.SetInputEnable(true);
		manager.DestroyManager();
        SceneManager.LoadScene(0); //LoadScene: carica la scena mediante indice del build settings
        Debug.Log("Carico il menu...");//piccolo controllo a runtime

    }

    //funzione che attiva il canvas di richiesta di uscita
    public void EsciDalGioco()
    {
        RichiestaExitUi.SetActive(true);
       // Application.Quit();
        //Debug.Log("Sto uscendo dal gioco...");
    }

    //funzione che permette di uscire dal gioco
    public void RispostaSi() {
        Application.Quit();
        Debug.Log("Sto uscendo dal gioco...");
    }

    //funzione che permette di chiudere il canvas Richiesta di uscita
    public void RispostaNo()
    {
        RichiestaExitUi.SetActive(false);
    }
}


