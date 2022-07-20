using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//questa classe permette di gestire tutto ciò che accade all'interno del gioco mediante gli appositi controlli
public class GameManager : MonoBehaviour {
	public bool entry;//booleano per il controllo dell'entrata
    public bool isFireOn = false; // booleano per il controllo della fiamma al centro dell hub
    public int playerLifeAtStart = 5; //vita piena
	public int playerLife; // varibile per aggiornare la vita del player
	private bool canJump = true; // booleano per il controllo del salto
    private bool inputEnabled = true; // booleano per il controllo degli input

    public bool rightEntranceRockDestroyed = false; // booleano per il controllo della roccia

    /*  Awake viene sempre chiamato prima di Start: se ad esempio in uno script su di un oggetto
        inseriamo una funzione Awake ed in un altro una Start, saremo sicuri che quando Start verrà
        chiamato l’Awake sarà già stato eseguito
    */
	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);/* Rende l'oggetto target non distrutto (in automatico) quando si carica una nuova scena.
                                                    Quando si carica un nuovo livello, tutti gli oggetti nella scena vengono distrutti, 
                                                    quindi vengono caricati gli oggetti nel nuovo livello. 
                                                    Per preservare un oggetto durante il caricamento di livello, 
                                                    si chiama DontDestroyOnLoad. 
                                                    Se l'oggetto è un componente o un oggetto di gioco, 
                                                    anche la sua intera gerarchia di trasformazione non verrà distrutta.
                                                */
		Application.targetFrameRate = 300;// indica che il gioco deve eseguire il rendering alla frequenza fotogrammi predefinita della piattaforma.
        playerLife = playerLifeAtStart; // assegnamo il valore della vita la player
	}

	void Start () {
		//niente start
	}

    //permette di caricare la scena indicata mediante input e chiude tutte le altre
	public void ButtonLoadscene(string sceneName)
	{
        
        //Carica le scene attraverso il nome o l'indice nella Build Settings.
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single); //loadSceneMode.Single: Chiude tutte le scene caricate e carica una scena.


    }


	// Update is called once per frame
	void Update () {
		//niente update
	}

    //setFuoco hub centrale
	public void SetFire(bool isOn) {
		isFireOn = isOn;
	}
    //ritorna un valore booleano se il fuoco è acceso o spento
	public bool GetFire() {
		return isFireOn;
	}

	public void SetEntry(bool other)
	{
		entry = other;
	}

	public bool GetEntry() {
		return entry;
	}

	public void SetPlayerLife(int lifeDelta) { //passa direttamente la differenza da sommare o sottrarre alla vita del personaggio
		playerLife += lifeDelta;
	}

    //ritorna la vita del giocatore a runtime
	public int GetPlayerLife() {
		return playerLife;
	}

    //resetta la vita al giocatore (vita al max)
	public void ResetPlayerLife() {
		playerLife = playerLifeAtStart;//assegna a playerLife il contenuto della variabile playerlifeatstart
	}

	public void SetInputEnable(bool state) {
		inputEnabled = state;
	}

	public bool GetInputEnable() {
		return inputEnabled;
	}

    //abilita o disabilita il salto al player
	public void SetJump(bool can) {
		canJump = can;
	}

	public bool GetJump() {
		return canJump;
	}
    //

    //funzione che permette di eliminare un gameobject dalla scena
	public void DestroyManager() {
		Destroy(gameObject);
		}
    //funzione che permette di resettare tutti i parametri a default
	public void ResetGame() {
		playerLife = playerLifeAtStart;
		rightEntranceRockDestroyed = false;
		isFireOn = false;
	}
}
