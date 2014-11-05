using UnityEngine;
using System.Collections;

public class OpeningCamera : MonoBehaviour {

	private Vector3 kiteString;
	private Vector3 startPos;
	private float startEulerX;
	private float startEulerY;
	private float startEulerZ;
	private Transform tf;
	private Transform playerTf;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		playerTf = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
		kiteString = tf.position - playerTf.position;
		startPos = tf.position;
		startEulerX = tf.rotation.eulerAngles.x;
		startEulerY = tf.rotation.eulerAngles.y;
		startEulerZ = tf.rotation.eulerAngles.z;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		tf.position = playerTf.position + kiteString;
		tf.rotation = Quaternion.Euler(-Vector3.SqrMagnitude(tf.position - startPos) * .030f + startEulerX, startEulerY, startEulerZ);
	}
}
