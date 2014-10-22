using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {

	Transform tf;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		// Debug: draw forward line
		Debug.DrawRay(tf.position, tf.forward, Color.white);
		// Check for player input

	}
	
}
