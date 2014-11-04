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
	public Vector3 cameraForward;
	public Vector3 cameraRight;
	private int stopTime;
	private int cameraMoveTime = 100;
	public bool movementBasedOnGravityVolumes = false;
	public bool usingPlayerCamera = true;
	public Transform nirvanaLockTf;
	public bool nirvanaLocked = false;
	public bool jumping = false;
	public float jumpForce = 30f;
	public Vector3 footOrigin = new Vector3(0f, -0.45f, 0f);
	public Animator boyAnimator;
	private Vector3 locVel;

	// Use this for initialization
	void Start () {
		if (usingPlayerCamera) {
			cameraTf = cameraPos.GetComponent<Transform>();
		}
		deadZone = 0.05f;
		tf = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();
		forwardVector = tf.forward;
		canMove = true;
		nirvanaLocked = false;
		stopTime = 0;
		targetPos = tf.rotation * kiteString + tf.position;
		targetRot = Quaternion.LookRotation(tf.rotation * -kiteString, -GetComponent<GravityHandler>().Gravity);
		cameraForward = Vector3.forward;
		cameraRight = Vector3.right;
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
		if (usingPlayerCamera) {
			cameraTf.position = targetPos;
			cameraTf.rotation = targetRot;
			cameraForward = lastAligned * Vector3.forward;
			cameraRight = lastAligned * Vector3.right;
		}
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
			Quaternion snapRot = tf.rotation;
			tf.rotation = Quaternion.LookRotation(forwardVector, -GetComponent<GravityHandler>().Gravity);
			// reduce sliding
			if (Mathf.Abs(Quaternion.Angle(snapRot, tf.rotation)) > 0.05f) {
				locVel = transform.InverseTransformDirection(rigidbody.velocity);
				locVel.x = 0;
				rigidbody.velocity = transform.TransformDirection(locVel);
			}
			// add the new forward velocity
			rb.AddForce(tf.forward * vel * playerScale);
			locVel = transform.InverseTransformDirection(rigidbody.velocity);
			if (locVel.z > maxVel * playerScale) {
				locVel.z = maxVel * playerScale;
			}
			rigidbody.velocity = transform.TransformDirection(locVel);
			stopTime = 0;
			boyAnimator.SetBool("Run", true);
		}
		// To make sure the player doesn't spin around randomly because of stupid physics reasons
		else {
			tf.rotation = Quaternion.LookRotation(forwardVector, -GetComponent<GravityHandler>().Gravity);
			stopTime++;
			boyAnimator.SetBool("Run", false);
		}
		if (stopTime >= cameraMoveTime) {
			lastAligned = tf.rotation;
		}
		if (!canMove && nirvanaLocked) {
			tf.position = nirvanaLockTf.position;
			jumping = true;
		}
		// Allow the player to jump
		if (Input.GetKeyDown(KeyCode.Space) && !jumping) {
			Debug.Log("Said Apple.");
			rb.AddForce(GetComponent<GravityHandler>().Gravity * -jumpForce * playerScale);
			jumping = true;
			boyAnimator.SetTrigger("JumpTrig");
			boyAnimator.SetBool("InAir", true);
		}
		// Check if the player has landed from a jump
		else if(Physics.Raycast(tf.rotation * footOrigin + tf.position, GetComponent<GravityHandler>().Gravity, 0.1f)) {
			if (jumping) {
				Debug.Log("The Eagle Has Landed.");
				jumping = false;
				boyAnimator.SetBool("InAir", false);
			}
		}
		Debug.DrawRay(tf.rotation * footOrigin + tf.position, GetComponent<GravityHandler>().Gravity * 0.1f, Color.white);
		// Remove any angular velocity AGAIN
		rb.angularVelocity = Vector3.zero;
	}
	
}
