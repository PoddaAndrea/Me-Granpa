using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    public float inputDelay = 0.1f;
    public float velocitaAvanti = 12;
    public float velocitaRotazione = 100;

    Quaternion targetRotazione;
    Rigidbody rBody;
    float inputAvanti, inputRotazione;

    public Quaternion TargetRotazione {
        get { return targetRotazione; }
    }

	// Use this for initialization
	void Start () {
        targetRotazione = transform.rotation;
        rBody = GetComponent<Rigidbody>();
        inputAvanti = inputRotazione = 0;
    }

    void GetInput() {
        inputAvanti = Input.GetAxis("Vertical");
        inputRotazione = Input.GetAxis("Horizontal");

    }

	// Update is called once per frame
	void Update () {
        GetInput();
        Turn();
        
	}

    void FixedUpdate()
    {
        Run();
    }

    void Run() {
        if (Mathf.Abs(inputAvanti)>inputDelay) {
            //muoviti!
            rBody.velocity = transform.forward * inputAvanti * velocitaAvanti;
        }
        else{
            //velocita a zero
            rBody.velocity = Vector3.zero;
        }
    }

    void Turn() {
        if (Mathf.Abs(inputRotazione) > inputDelay) {
            targetRotazione *= Quaternion.AngleAxis(velocitaRotazione * inputRotazione * Time.deltaTime, Vector3.up);
        }

        transform.rotation = targetRotazione;
    }
}
