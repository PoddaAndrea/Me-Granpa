using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public GameObject deathParticles;
	private GameObject Player;
	//public ParticleSystem deathParticles;
	[System.NonSerialized]
	public Transform Character; // Target Object to follow
	public GameObject deathAudioClip;
	public float speed = 0.1F; // Enemy speed
	private float speedBackup;
	public float stunTime = 3.0f;

	private Vector3 directionOfCharacter;

	[System.NonSerialized]
	public bool challenged = false;// If the enemy is Challenged to follow by the player

	public bool stun;
	public bool secondStun;

	public int life = 2;
	public int damage = 1;
	private bool unstunnable = false;

	private Animator enemyAnim;
	private Rigidbody rb;
	private AudioSource audioSourceComponent;
	public AudioClip clipHit;

	void Start() {
		Player = GameObject.FindWithTag("Player");
		Character = Player.transform;
		speedBackup = speed;
		enemyAnim = GetComponent<Animator>();
		audioSourceComponent = GetComponent<AudioSource>();
		rb = GetComponent<Rigidbody>();
	}


	void Update()
	{
		if (challenged) //se entra il giocatore nel suo sensore lo rincorre
		{
			
			directionOfCharacter = Character.transform.position - transform.position;
			directionOfCharacter = directionOfCharacter.normalized;    // Get Direction to Move Towards
			transform.Translate(directionOfCharacter * speed, Space.World);

			enemyAnim.SetBool("isMoving", true); //settare il false da qualache parte


		}

		if (stun && !secondStun) { //se stunnato una sola volta
			secondStun = true;
			StartCoroutine(WaitStunToFinish(stunTime));
			stun = false;
		}

		if (stun && secondStun && !unstunnable){//se stunnato due volte, massimo 2 stun
			StartCoroutine(WaitStunToFinish(stunTime / 2.0f));
			unstunnable = true;
		}
			


	}

	IEnumerator WaitStunToFinish(float stunDeltaTime) { 
		speed = 0.0f;
		yield return new WaitForSecondsRealtime(stunDeltaTime);
		speed = speedBackup;
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Sword")) {
			PlayDamage();
			life -= damage;
			enemyAnim.SetBool("isAttacked", true);
		}

		if (life <= 0)
		{
			//deathParticles.Play();
			GameObject deathAudio = Instantiate(deathAudioClip, transform.position, Quaternion.identity);
			GameObject particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
		}

	}

	void PlayDamage()
	{
		AudioSource.PlayClipAtPoint(clipHit, rb.position);	
	}
}