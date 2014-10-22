﻿using UnityEngine;
using System.Collections;

public class GravityHandler : MonoBehaviour {
	
	Rigidbody rigBody;
	
	Vector3 Gravity;//Down direction for this gameObject. Set on Trigger Event
	
	// Use this for initialization
	void Start () {
		rigBody = GetComponent<Rigidbody>();
		
		Gravity = new Vector3(0f, -1f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		rigBody.AddForce(Gravity);
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "DynGravVolume") {
			Debug.Log("dynGrav");
			Gravity = other.gameObject.GetComponent<DynamicGravityDirection>().GravDirection;
		}
		if (other.gameObject.tag == "AbsGravVolume") {
			Debug.Log("absGrav");
			Gravity = other.gameObject.GetComponent<AbsoluteGravityDirectiion>().GravDirection;
		}
	}
}