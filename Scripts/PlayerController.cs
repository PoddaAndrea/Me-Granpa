using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //varibili generali del PlayerController
    public float speed = 0.0f;
	public float jumpIntensity = 0.0f;
	public float dashIntensity;
	public float stompIntensity;
	public float shakeTime = 0.5f;
	public Camera myCamera;
	private float velocityBackup;
	Vector3 direction = new Vector3(0, 0, 0);
	private Vector3 granPaPosition;
	private Vector3 oldPosition;
	private Quaternion oldRotation;
	public int pushForce;
    //

    //varibili Audio playerController
	public AudioClip clipGranpa;
	public AudioClip clipHit;
	public AudioClip clipJump;
	public AudioClip clipAttack;
	public AudioClip clipWhenHit;
	public AudioClip clipRoll;
    //

    //varibili del game over
	public Canvas gameOver;
    //

	private float damTimer;


	public AudioSource audioSourceComponent; //variabile di tipo AudioSource

	private Rigidbody rb;
	//private Rigidbody granPaRb;
	private Rigidbody swordRb;

	private Animator granPaAnim;
	private Animator playerAnim;

	private float hMovement;
	private float vMovement;
	private bool isGrounded = true;

	public GameObject granPaSignal;
	public GameObject granPa;
	public GameObject sword;

	private bool managerFound = false;
	private int enemyDamage = 1;

	private GameObject gameManagerObject;
	private GameManager manager;


	// Use this for initialization
	void Start () {
		audioSourceComponent = GetComponent<AudioSource>();
		rb = GetComponent<Rigidbody>();
		//granPaRb = granPa.GetComponent<Rigidbody>();
		granPaAnim = granPa.GetComponent<Animator>();
		//granPaPosition = granPaRb.position;

		swordRb = sword.GetComponent<Rigidbody>();
		playerAnim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//solito controllo del game manager
		if (!managerFound) {
			gameManagerObject = GameObject.FindWithTag("GameManager");
			manager = gameManagerObject.GetComponent<GameManager>();
			managerFound = true;
		}

		if (manager.GetInputEnable()) //se gli input per il giocatore sono attivati
		{
			Vector3 NextDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); //muove il personaggio nella direzione in cui si muove
					if (NextDir != Vector3.zero)
						transform.rotation = Quaternion.LookRotation(NextDir);
					if(damTimer > 0)
					{
			    		 damTimer -= Time.deltaTime;
			  		 }
			hMovement = Input.GetAxis("Horizontal");
			vMovement = Input.GetAxis("Vertical");

			rb.velocity = new Vector3(hMovement * speed, rb.velocity.y, vMovement * speed); //movimento orizzontale

			if(rb.velocity.sqrMagnitude > speed){
				velocityBackup = rb.velocity.y;
        		rb.velocity = rb.velocity.normalized * speed; //evita di moltiplicare le velocità quando si muove in diagonale
				rb.velocity = new Vector3(rb.velocity.x,velocityBackup,rb.velocity.z);
   			}

			if (rb.velocity == new Vector3(0.0f, rb.velocity.y, 0.0f)) //se il Player è fermo metto isMoving a false
			{
				playerAnim.SetBool("isMoving", false);//disattiva l'animazione del player
				audioSourceComponent.Stop();// disattiva l'audio

			}

			else
			{
				playerAnim.SetBool("isMoving", true); //attiva l'animazione del player
				//if(!audio.isPlaying)
				//audio.PlayOneShot(audio.clip);
			}

			if (!granPaAnim.GetBool("buttonPressed")){
				granPaSignal.SetActive(false);
			}
			


            //script che permette al player di saltare
			if (Input.GetButtonDown("Jump") && manager.GetJump() && isGrounded == true) //salto
			{                       //salto
				rb.AddForce(new Vector3(0f, jumpIntensity, 0f), ForceMode.VelocityChange);
				AudioSource.PlayClipAtPoint(clipJump, rb.position);//attiva l'audio del salto
				playerAnim.SetBool("onGround", false); //quando effettua il salto onground è a false
				isGrounded = false;//imposta Isgraounded a false
			}
            //

            //script che permette al player di attaccare
			if (Input.GetButtonDown("Fire1")) //attacco
			{
				playerAnim.SetBool("attackButtonPressed", true); //attiva l'animazione di attacco
				//AudioSource.PlayClipAtPoint(clipAttack, rb.position);
				//sword.GetComponent<SphereCollider>().enabled = true;

			}
            //

            //script che permette a Granpa di attaccare
			if (Input.GetButtonDown("Fire2") && isGrounded) //colpo del nonno secondo gli input prestabiliti da default
			{ //granpa
				granPaSignal.SetActive(true);

				granPaAnim.SetBool("buttonPressed", true); // controllo booleano sulla pressiione del bottone
				AudioSource.PlayClipAtPoint(clipGranpa, rb.position, .8f); //attivazione audio
				StartCoroutine(WaitCameraShake());//richiama la funzione del camera shake mediante Couroutine
			}
            //

		}

        //script che permette di disabilitare l'animazione e il suono quando il player è fermo
		if(!manager.GetInputEnable()) { //se input è disattivato fermo il giocatore
			playerAnim.SetBool("isMoving", false);
			rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
		}
        //


	}

    //script per la gestione delle collisioni

    void OnCollisionEnter(Collision other) {
        //controllo quando il player ha i piedi in terra :)
		if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Cubes"))
			isGrounded = true;
			playerAnim.SetBool("onGround", true);
        //

        //controllo attacco dei nemici
		if (other.gameObject.CompareTag("Enemy") && damTimer <= 0) { 
            Damage(enemyDamage); //danno che procurano i nemici
			Debug.Log("colpito");//controllo a runtime
			rb.AddForce(other.transform.forward * pushForce, ForceMode.VelocityChange);// viene applicata una forza di attacco
			playerAnim.SetBool("isAttacked", true);//attiva l'animazione di attacco
		}
        //
		
        //controllo attacco dei nemici con scudo
		if (other.gameObject.CompareTag("EnemyWithShield")){ 
            Damage(enemyDamage);//danno che procurano i nemici
            playerAnim.SetBool("isAttacked", true); //attiva l'animazione di attacco
		}
        //


        //controllo attacco del Boss
		if (other.gameObject.CompareTag("Boss")){ 
            Damage(2); //danno che procura il boss
			playerAnim.SetBool("isAttacked", true); //attiva l'animazione di attacco
		}
        //
	}
    //

    //funzione del danno
	void Damage(int damageAmount) {
		AudioSource.PlayClipAtPoint(clipWhenHit, rb.position);//attiva suono del danno
		manager.SetPlayerLife(-damageAmount);//decremento della vita

        //se la vita è minore o uguale a zero attivo la funzione di gameOver
		if (manager.GetPlayerLife() <= 0) {
			GameOver();
		}
		damTimer = 1.0f;
        //
	}
    //

    //funzione game over
	public void GameOver() {
		Debug.Log("Dead");
		manager.SetInputEnable(false); //disabilito l'input del giocatore

		gameOver.GetComponent<Animator>().SetBool("FadeIn", true); //attivo l'animazione del FadeIn

		gameObject.SetActive(false); //disabilito tutte le funzioni del player
	}
    //

    //script camera shake

    //IEnumerator afferma di avere una collezione di valori che è possibile richiedere uno per uno, un po 'come una List
    IEnumerator WaitCameraShake() {//disattiva controlli avvia lo shake della camera poi ridà i controlli resettando la posizione e rotazione della camera
		oldPosition = myCamera.GetComponent<Transform>().position;
		oldRotation = myCamera.GetComponent<Transform>().rotation;
		manager.SetInputEnable(false);
		rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);

		myCamera.GetComponent<CameraShake>().isShaking = true;


		yield return new WaitForSecondsRealtime(shakeTime); //aspetta il tempo dello shakeTime prima di fare qualcos'altro


		myCamera.GetComponent<CameraShake>().Disable();
		granPaSignal.SetActive(false);
		manager.SetInputEnable(true);
		myCamera.GetComponent<Transform>().position = oldPosition;
		myCamera.GetComponent<Transform>().rotation = oldRotation;
	}
    //

    //funzioni audio generali

	void PlayFootsteps() { 
		audioSourceComponent.PlayOneShot(audioSourceComponent.clip); //riproduce l'audio e ridimensiona il volume tramite volumeScale
	}
	void PlayAttack()
	{
		AudioSource.PlayClipAtPoint(clipAttack, rb.position, 0.3f);	//riproduce l'audio in una determinata parte del mondo e viene eliminata una volta riprodotta tutta
		
	}

    void PlayDamage()
    {
	    AudioSource.PlayClipAtPoint(clipHit, rb.position); //riproduce l'audio in una determinata parte del mondo e viene eliminata una volta riprodotta tutta
    }

    //
}
