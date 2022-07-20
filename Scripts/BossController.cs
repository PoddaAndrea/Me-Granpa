using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
	private bool hitWall = false;
	private bool managerFound = false;
	private GameObject gameManagerObject;
	private GameManager manager;
	public Camera myCamera;
	public GameObject Player;
	private Rigidbody playerBody;
	public GameObject sensorObject;
	private SenseController sensor;
	private Rigidbody rb;
	public float speed; // Enemy speed
	public float stunTime = 4f;
	private float pushForce = 250.0f;
	public int life;
	public int damage = 1;
	public bool lookingForPlayer = true;
	private Animator anim;
	private AudioSource audioS;
	public AudioClip hit;
	public AudioClip runClip;
	public float shakeTime = 0.5f;
	private Vector3 oldPosition;
	private Quaternion oldRotation;

	void Start()
	{
		playerBody = Player.GetComponent<Rigidbody>();
		audioS = GetComponent<AudioSource>();
		anim = gameObject.GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		sensor = sensorObject.GetComponent<SenseController>();
	}


	void FixedUpdate()
	{
		if (!managerFound) {
			gameManagerObject = GameObject.FindWithTag("GameManager");
			manager = gameManagerObject.GetComponent<GameManager>();
			managerFound = true;
		}

		if(lookingForPlayer)//se è nella fase di ricerca personaggio allora fa il dash verso di lui
		{
			Dash(Player.transform);
		}
				
		if (life <= 0)
            this.gameObject.SetActive(false);
		if (sensor.GetGranpa()) {
			sensor.SetGranpa(false);
            StartCoroutine(WaitStunToFinish());
		}

		if (manager.GetPlayerLife() <= 0) { 
            gameObject.SetActive(false);
		}

	}

	void Dash(Transform direction) {
		transform.LookAt(direction);
		StartCoroutine(ResetWallHit());
		anim.SetBool("isRunning", true);
		rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
		lookingForPlayer = false;
	}

	IEnumerator ResetWallHit() { //evita i colpire continuamente l'ultimo muro che ha colpito
		yield return new WaitForSecondsRealtime(1f);
		hitWall = false;

	}




	void OnCollisionEnter(Collision other)//vari comportamenti in base a cosa colpisce
	{
		Debug.Log(other.gameObject.tag);
		if (other.gameObject.CompareTag("Wall")) {
			Debug.Log("colpito muro");

			if (!hitWall)
			{
				hitWall = true;
				rb.velocity = transform.forward * -pushForce;
				StartCoroutine(WaitCameraShake());
				StartCoroutine(WaitStunToFinish());
			}
		}

		if (other.gameObject.CompareTag("Player")) {
			Debug.Log("colpito giocatore");
			playerBody.AddForce(gameObject.transform.forward * pushForce, ForceMode.Acceleration); //spinge il giocatore quando lo colpisce
            StartCoroutine(WaitCameraShake());
            StartCoroutine(WaitStunToFinish());
		}

		if (other.gameObject.CompareTag("Granpa")) {
			Debug.Log("colpito da nonno");
            StartCoroutine(WaitStunToFinish());
		}

		if (other.gameObject.CompareTag("Stalactites"))
		{
			
			audioS.PlayOneShot(hit);
			life --;
			if (life <= 0) { 
				SceneManager.LoadScene("WinState");
			}
			other.gameObject.SetActive(false);
		}



	}

	IEnumerator WaitStunToFinish()
	{
		rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
		anim.SetBool("isRunning", false);
		rb.isKinematic = true;
		audioS.PlayOneShot(hit);

		yield return new WaitForSecondsRealtime(stunTime);

		Debug.Log("Uscito");
		lookingForPlayer = true;
		rb.isKinematic = false;	
	}

	IEnumerator WaitCameraShake()
	{
		oldPosition = myCamera.GetComponent<Transform>().position;
		oldRotation = myCamera.GetComponent<Transform>().rotation;
		manager.SetInputEnable(false);

		myCamera.GetComponent<CameraShake>().isShaking = true;

		yield return new WaitForSecondsRealtime(shakeTime);

		myCamera.GetComponent<CameraShake>().Disable();
		manager.SetInputEnable(true);
		myCamera.GetComponent<Transform>().position = oldPosition;
		myCamera.GetComponent<Transform>().rotation = oldRotation;	
	
	}

	public void RunSound() {
		audioS.PlayOneShot(runClip);
	}

}