//Author: Dom Cristaldi
//Game: Transcendance

using UnityEngine;
using System.Collections;

public class GravityHandler : MonoBehaviour {

	Transform tf;
	Rigidbody rigBody;

	public Vector3 Gravity;//Down direction for this gameObject. Set on Trigger Event
	//public Vector3 GravityHolder;
	public Vector3 gravForward;
	public Vector3 gravRight;

	public float GravityMagnitude = 9.8f;
	public float TerminalVelocity = 5.0f;
	
	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		rigBody = GetComponent<Rigidbody>();
		
		Gravity = new Vector3(0f, -1f, 0f) * GravityMagnitude;
		gravForward = Vector3.forward;
		gravRight = Vector3.right;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		rigBody.AddForce(Gravity);

		rigBody.velocity = Vector3.ClampMagnitude(rigBody.velocity, TerminalVelocity);
	}

	public void applyNewGravDirection(Vector3 gravDirection, Quaternion gravRotation) {
		//rigBody.isKinematic = true;//stop all forces on object
		//rigBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
		rigBody.velocity = (gravRotation) * rigBody.velocity;
		Gravity = gravDirection;//set gravity
		//rigidbody.isKinematic = false;//resume forces
	}
}
