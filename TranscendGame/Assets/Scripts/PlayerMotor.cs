using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {

	private float hAxis;
	private float vAxis;
	public float deadZone = 0.05f;
	public float vel = 2.0f;
	public float maxVel = 8.0f;
	public static float playerScale = 1.0f;
	private Vector3 forwardVector;
	private Transform tf;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		deadZone = 0.05f;
		tf = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();
		forwardVector = tf.forward;
	}
	
	// Update is called once per frame
	void Update () {
		// Debug: draw forward line
		Debug.DrawRay(tf.position, tf.forward * 5, Color.blue);
		// Check for player input
		hAxis = Input.GetAxis("Horizontal");
		vAxis = Input.GetAxis("Vertical");
		// Vector calculations
		if (!(Mathf.Abs(hAxis) < deadZone && Mathf.Abs(vAxis) < deadZone)) {
			forwardVector = GetComponent<GravityHandler>().gravForward * vAxis + GetComponent<GravityHandler>().gravRight * hAxis;
			if (forwardVector.magnitude > 1.0f) {
				forwardVector.Normalize();
			}
			Debug.DrawRay(tf.position, forwardVector * vel * playerScale, Color.red);
			tf.rotation = Quaternion.LookRotation(forwardVector, GetComponent<GravityHandler>().Gravity);
			rb.AddForce(tf.forward * vel * playerScale);
		}
		else {
			tf.rotation = Quaternion.LookRotation(forwardVector, GetComponent<GravityHandler>().Gravity);
		}
	}
	
}
