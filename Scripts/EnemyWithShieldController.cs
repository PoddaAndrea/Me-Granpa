using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithShieldController : MonoBehaviour
{
	public GameObject deathAudioClip; // variabile di tipo GameObject (audio morte)
	public GameObject deathParticles; // variabile di tipo GameObject (particelle morte)
    private GameObject Player; // variabile di tipo GameObject
    [System.NonSerialized]
	public Transform Character; // è il target da seguire
	private Quaternion oppositeCharacter; // variabile di tipo quaternione
    private bool challenged = false;
	public float speed = 0.1F; // velocita dei nemici
    public GameObject shield; // variabile di tipo GameObject (scudo)
	private float speedBackup;
	public float stunTime = 3.0f;
	private bool shieldDown = false;
	private Vector3 directionOfCharacter;
    //public ParticleSystem deathParticles;
    
        
     /*L'attributo NonSerializable contrassegna una variabile per non essere serializzata.
     *In questo modo puoi mantenere una variabile pubblica e 
     * Unity non tenta di serializzarlo o 
     * mostrarlo nell'Inspector
     */
    [System.NonSerialized] 
    public bool stun;

	private bool secondStun;

	public int life = 2;
	public int damage = 1;
	private bool unstunnable = false;

	private Animator anim;

	public SenseController sensor;

	void Start()
	{
		Player = GameObject.FindWithTag("Player");
		Character = Player.transform;
		speedBackup = speed;
		anim = gameObject.transform.Find("mummy_rig").GetComponent<Animator>();
	}


	void FixedUpdate()
	{
		if (sensor.GetPlayer())
			challenged = true;

		if (challenged)
		{

			directionOfCharacter = Character.transform.position - transform.position;
			directionOfCharacter = directionOfCharacter.normalized;    // Get Direction to Move Towards

			transform.Translate(directionOfCharacter * speed, Space.World);

			transform.LookAt(Character);

			anim.SetBool("crippled", true);


		}

		if (sensor.GetGranpa() && !stun) {
			stun = true;
			sensor.SetGranpa(false);
			shield.SetActive(false);
			shieldDown = true;
			StartCoroutine(WaitStunToFinish(stunTime));
		}

		if (sensor.GetGranpa() && stun && !unstunnable) {
            StartCoroutine(WaitStunToFinish(stunTime / 2.0f));
			unstunnable = true;
		}



	}

	IEnumerator WaitStunToFinish(float stunDeltaTime)
	{
		speed = 0.0f;
		yield return new WaitForSecondsRealtime(stunDeltaTime);
		speed = speedBackup;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Sword"))
		{
			if (shieldDown)
				life -= damage;
		}


		if (life <= 0)
		{
			GameObject deathAudio = Instantiate(deathAudioClip, transform.position, Quaternion.identity);
			GameObject particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
			this.gameObject.SetActive(false);
		}

	}
}