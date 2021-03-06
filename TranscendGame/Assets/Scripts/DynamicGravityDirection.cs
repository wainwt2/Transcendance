﻿//Author: Dom Cristaldi
//Game: Transcendance

using UnityEngine;
using System.Collections;

public class DynamicGravityDirection : MonoBehaviour {
	
	Transform tf;
	BoxCollider collider;
	
	GameObject Player;
	
	public Vector3 GravDirection;
	public float GravScale = 1f;
	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		collider = GetComponent<BoxCollider>();
		
		Player = GameObject.FindGameObjectWithTag("player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GravDirection = -tf.up;
		GravDirection = new Vector3(GravScale * GravDirection.x,
		                            GravScale * GravDirection.y,
		                            GravScale * GravDirection.z);
		
		//Debug.Log(GravDirection);
		
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "player") {
			Debug.Log("boom");
			other.gameObject.GetComponent<GravityHandler>().applyNewGravDirection(GravDirection, tf.rotation);
			other.gameObject.GetComponent<GravityHandler>().gravForward = tf.forward;
			other.gameObject.GetComponent<GravityHandler>().gravRight = tf.right;
		}
	}
	/*
	void SetGravity(GameObject gravBody) {
		gravBody.GetComponent<GravityHandler>().applyNewGravDirection(GravDirection, tf.rotation);
		gravBody.GetComponent<GravityHandler>().gravForward = tf.forward;
		gravBody.GetComponent<GravityHandler>().gravRight = tf.right;
	}
	*/
	/*
	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawCube(collider.center, collider.size);
	}
	*/
}