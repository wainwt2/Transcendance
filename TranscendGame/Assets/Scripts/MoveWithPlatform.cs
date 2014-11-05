using UnityEngine;
using System.Collections;

public class MoveWithPlatform : MonoBehaviour {

	Transform tf;

	Vector3 prevPos;

	Vector3 deltaPos;

	Vector3 curPos;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();

		prevPos = tf.position;
	
	}
	
	// Update is called once per frame
	void Update () {
		curPos = tf.position;//get current position at start of update
		
		deltaPos = curPos - prevPos;//get difference in position
		
		prevPos = tf.position;//set previous position at end of update
	}

	void OnTriggerStay(Collider other) {
		Debug.Log(other.tag);
		other.gameObject.GetComponent<Transform>().position += deltaPos;
	}
}
