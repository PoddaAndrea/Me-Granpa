using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giocatore : MonoBehaviour {
    //public Animator anim;
    public GameObject cartelloUI;
    public float inputDelay = 0.1f;
    public float velocitaAvanti = 12;
    public float velocitaRotazione = 100;

    Quaternion targetRotazione;
    Rigidbody rBody;
    public float inputAvanti, inputRotazione;


    public Quaternion TargetRotazione
    {
        get { return targetRotazione; }
    }
    // Use this for initialization
    void Start () {
        targetRotazione = transform.rotation;
        rBody = GetComponent<Rigidbody>();
        inputAvanti = inputRotazione = 0;
        //anim = GetComponent<Animator>();
	}

    void GetInput()
    {
        inputAvanti = Input.GetAxis("Vertical");
        inputRotazione = Input.GetAxis("Horizontal");

    }

    // Update is called once per frame
    void Update ()
    {

        GetInput();

        Turn();

       /* if (Input.GetMouseButtonDown(0))
        {
            
                int n = Random.Range(0, 2);
                if (n == 0)
                {
                    //anim.Play("DoppiPugni", -1, 0f);
                }
                else {
                    anim.Play("Calcio", -1, 0f);
                }


            
        }*/


	}

    void FixedUpdate()
    {
        Run();
    }

    void OnTriggerEnter(Collider obj)
    {

        if (obj.tag == "cartello")
        {
            cartelloUI.SetActive(true);
            Debug.Log("lettura del cartello");
        }
  
    }

    private void OnTriggerExit(Collider obj)
    {
        cartelloUI.SetActive(false);
        Debug.Log("sono uscito dal collider del cartello");
    }

    void Run()
    {
        if (Mathf.Abs(inputAvanti) > inputDelay)
        {
            //muoviti!
            rBody.velocity = transform.forward * inputAvanti * velocitaAvanti;
           
           // anim.Play("Walk", -1, 0f);
        }
        else
        {
            //velocita a zero
            rBody.velocity = Vector3.zero;
        }
    }

    void Turn()
    {
        if (Mathf.Abs(inputRotazione) > inputDelay)
        {
            targetRotazione *= Quaternion.AngleAxis(velocitaRotazione * inputRotazione * Time.deltaTime, Vector3.up);
        }

        transform.rotation = targetRotazione;
    }

}
