﻿using UnityEngine;
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
	private Quaternion locRot;
	private Quaternion locRot2;
	private Quaternion locRot3;
	private Vector3 gravSnap;
	private Vector3 gravForSnap;
	private Vector3 gravRigSnap;
	private Vector3 forSnap;
	private Vector3 rigSnap;
	private RaycastHit[] rHit;

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
		lastAligned = tf.rotation;
		tf.localScale = new Vector3(playerScale, playerScale, playerScale);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Debug: draw forward line
		//Debug.DrawRay(tf.position, tf.forward * 5, Color.blue);
		//Debug.DrawRay(tf.position, tf.up * 5, Color.green);
		// realign rotation to gravity
		if (GetComponent<GravityHandler>().Gravity != gravSnap) {
			//locRot = Quaternion.FromToRotation(gravForSnap, forSnap);
			//Debug.DrawRay(tf.position, locRot * GetComponent<GravityHandler>().gravForward * 10f, Color.yellow);
			//tf.rotation = Quaternion.LookRotation(locRot * GetComponent<GravityHandler>().gravForward, -GetComponent<GravityHandler>().Gravity);
			//tf.rotation = tf.rotation * Quaternion.Inverse(locRot);
			//locRot = Quaternion.FromToRotation(gravSnap, GetComponent<GravityHandler>().Gravity);
			//locRot2 = Quaternion.FromToRotation(Vector3.Cross(gravRigSnap, gravForSnap), Vector3.Cross(cameraRight, cameraForward));
			//locRot3 = Quaternion.FromToRotation(gravForSnap, cameraForward);
			//tf.rotation = Quaternion.LookRotation(locRot * tf.forward, -GetComponent<GravityHandler>().Gravity);
			//lastAligned = lastAligned * locRot * locRot2;
			lastAligned = Quaternion.LookRotation(GetComponent<GravityHandler>().gravForward, -GetComponent<GravityHandler>().Gravity);
		}
		// Debug: Constantly update target camera position and rotation
		//lastAligned = tf.rotation;
		// Update target camera position
		Debug.DrawRay(tf.position, lastAligned * Vector3.forward);
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
				locVel = tf.InverseTransformDirection(rb.velocity);
				locVel.x = 0;
				rb.velocity = tf.TransformDirection(locVel);
			}
			// add the new forward velocity
			rb.AddForce(tf.forward * vel * playerScale);
			locVel = tf.InverseTransformDirection(rb.velocity);
			if (locVel.z > maxVel * playerScale) {
				locVel.z = maxVel * playerScale;
			}
			rb.velocity = tf.TransformDirection(locVel);
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
		rHit = Physics.RaycastAll(tf.rotation * (footOrigin * playerScale) + tf.position, GetComponent<GravityHandler>().Gravity, 0.1f);
		// Allow the player to jump
		if (Input.GetKeyDown(KeyCode.Space) && !jumping) {
			Debug.Log("Said Apple.");
			rb.AddForce(GetComponent<GravityHandler>().Gravity * -jumpForce * playerScale);
			jumping = true;
			boyAnimator.SetTrigger("JumpTrig");
			boyAnimator.SetBool("InAir", true);
		}
		// Check if the player has landed from a jump
		else if (rHit.LongLength > 0) {
			foreach (RaycastHit rh in rHit) {
				if (jumping && !rh.collider.isTrigger) {
					Debug.Log("The Eagle Has Landed.");
					jumping = false;
					boyAnimator.SetBool("InAir", false);
				}
			}
		}
		else if (!jumping && rHit.LongLength == 0){
			Debug.Log("OH GOD WHERE DID THE GROUND GO");
			jumping = true;
			boyAnimator.SetBool("InAir", true);
		}
		Debug.DrawRay(tf.rotation * footOrigin + tf.position, GetComponent<GravityHandler>().Gravity * 0.1f, Color.white);
		// Remove any angular velocity AGAIN
		rb.angularVelocity = Vector3.zero;
		gravSnap = GetComponent<GravityHandler>().Gravity;
		gravForSnap = GetComponent<GravityHandler>().gravForward;
		gravRigSnap = GetComponent<GravityHandler>().gravRight;
		forSnap = tf.forward;
		rigSnap = tf.right;
	}

	public void RescalePlayer(float newScale) {
		playerScale *= newScale;
		tf.localScale = new Vector3(playerScale, playerScale, playerScale);
		if (GetComponentInChildren<PlayerTrigger>().heldItem != null) {
			GetComponentInChildren<PlayerTrigger>().heldItem.GetComponent<Transform>().localScale *= newScale;
		}
	}

}
