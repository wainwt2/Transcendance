//Author: Dom Cristaldi
//Game: Transcendance

using UnityEngine;
using System.Collections;

public class AbsoluteGravityDirection : MonoBehaviour {
	
	Transform tf;
	BoxCollider collider;
	
	GameObject Player;
	GameObject[] GravBodies;

	Quaternion rotSnap;
	
	public Vector3 GravDirection;
	public float GravScale = 1f;
	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		collider = GetComponent<BoxCollider>();
		
		Player = GameObject.FindGameObjectWithTag("player");
		GravBodies = GameObject.FindGameObjectsWithTag("gravBody");
		rotSnap = tf.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GravDirection = -tf.up;
		GravDirection = new Vector3(GravScale * GravDirection.x,
		                            GravScale * GravDirection.y,
		                            GravScale * GravDirection.z);
		if (tf.rotation != rotSnap) {
			Debug.Log("The world is spinning!");
			foreach (GameObject gravBody in GravBodies) {
				if (gravBody != null)
				SetGravity(gravBody);
			}
			SetGravity(Player);
		}
		//Debug.Log(GravDirection);
		Debug.DrawRay(tf.position, tf.up * -5, Color.black);
		Debug.DrawRay(tf.position, tf.forward * 5, Color.green);
		rotSnap = tf.rotation;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "player") {
			other.gameObject.GetComponent<GravityHandler>().applyNewGravDirection(GravDirection, tf.rotation);
			other.gameObject.GetComponent<GravityHandler>().gravForward = tf.forward;
			other.gameObject.GetComponent<GravityHandler>().gravRight = tf.right;
		}
		if (other.gameObject.tag == "gravBody") {
			other.gameObject.GetComponent<GravityHandler>().applyNewGravDirection(GravDirection, tf.rotation);
			other.gameObject.GetComponent<GravityHandler>().gravForward = tf.forward;
			other.gameObject.GetComponent<GravityHandler>().gravRight = tf.right;
		}
	}

	public void SetGravity(GameObject gravBody) {
		gravBody.GetComponent<GravityHandler>().applyNewGravDirection(GravDirection, tf.rotation);
		gravBody.GetComponent<GravityHandler>().gravForward = tf.forward;
		gravBody.GetComponent<GravityHandler>().gravRight = tf.right;
	}

	/*
	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawCube(collider.center, collider.size);
	}
	*/
}
