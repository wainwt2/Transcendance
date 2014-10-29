using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {

	private float hAxis;
	private float vAxis;
	public float deadZone = 0.05f;
	public float axisExponent = 3.0f;
	public float vel = 2.0f;
	public float maxVel = 8.0f;
	public static float playerScale = 1.0f;
	private Vector3 forwardVector;
	private Transform tf;
	private Rigidbody rb;
	public bool canMove;
	public Quaternion lastAligned;
	public Vector3 kiteString;
	public GameObject cameraPos;
	private Transform cameraTf;
	private Quaternion targetRot;
	private Vector3 targetPos;
	private Vector3 cameraForward;
	private Vector3 cameraRight;
	private int stopTime;
	private int cameraMoveTime = 100;
	public bool movementBasedOnGravityVolumes = false;

	// Use this for initialization
	void Start () {
		deadZone = 0.05f;
		tf = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();
		forwardVector = tf.forward;
		canMove = true;
		stopTime = 0;
		targetPos = tf.rotation * kiteString + tf.position;
		targetRot = Quaternion.LookRotation(tf.rotation * -kiteString, -GetComponent<GravityHandler>().Gravity);
		cameraTf = cameraPos.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Debug: draw forward line
		Debug.DrawRay(tf.position, tf.forward * 5, Color.blue);
		Debug.DrawRay(tf.position, tf.up * 5, Color.green);
		// Debug: Constantly update target camera position and rotation
		//lastAligned = tf.rotation;
		// Update target camera position
		targetPos = lastAligned * kiteString + tf.position;
		targetRot = Quaternion.LookRotation(lastAligned * -kiteString, -GetComponent<GravityHandler>().Gravity);
		// Remove any angular velocity
		rb.angularVelocity = Vector3.zero;
		// Set camera position to target position
		cameraTf.position = targetPos;
		cameraTf.rotation = targetRot;
		cameraForward = lastAligned * Vector3.forward;
		cameraRight = lastAligned * Vector3.right;
		// Check for player input
		hAxis = Input.GetAxis("Horizontal");
		vAxis = Input.GetAxis("Vertical");
		// Vector calculations
		if (!(Mathf.Pow(Mathf.Abs(hAxis), axisExponent) < deadZone && Mathf.Pow(Mathf.Abs(vAxis), axisExponent) < deadZone) && canMove){
			if (movementBasedOnGravityVolumes) {
				forwardVector = GetComponent<GravityHandler>().gravForward * Mathf.Pow(vAxis, axisExponent) 
					+ GetComponent<GravityHandler>().gravRight * Mathf.Pow(hAxis, axisExponent);
			}
			else {
				forwardVector = cameraForward * Mathf.Pow(vAxis, axisExponent) + cameraRight * Mathf.Pow(hAxis, axisExponent);
			}
			if (forwardVector.magnitude > 1.0f) {
				forwardVector.Normalize();
			}
			Debug.DrawRay(tf.position, forwardVector * vel * playerScale, Color.red);
			tf.rotation = Quaternion.LookRotation(forwardVector, -GetComponent<GravityHandler>().Gravity);
			rb.AddForce(tf.forward * vel * playerScale);
			stopTime = 0;
		}
		// To make sure the player doesn't spin around randomly because of stupid physics reasons
		else {
			tf.rotation = Quaternion.LookRotation(forwardVector, -GetComponent<GravityHandler>().Gravity);
			stopTime++;
		}
		if (stopTime >= cameraMoveTime) {
			lastAligned = tf.rotation;
		}
	}
	
}
