using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviour : MonoBehaviour {
    public int followSpeed = 1;
    private GameObject goTarget;
    private Rigidbody rbAgent;

	// Use this for initialization
	void Start () {
        goTarget = GameObject.Find("Drago");
        rbAgent = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dirForce = goTarget.transform.position - rbAgent.position;
        rbAgent.AddForce(Vector3.Normalize(dirForce)*followSpeed);
       
	}
}
