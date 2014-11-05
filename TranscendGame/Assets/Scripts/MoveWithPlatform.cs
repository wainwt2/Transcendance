using UnityEngine;
using System.Collections;

public class MoveWithPlatform : MonoBehaviour {

	GameObject Subject;

	Transform tf;

	Vector3 prevPos;

	Vector3 deltaPos;

	Vector3 curPos;

	bool applyDelta = false;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();

		prevPos = tf.position;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		curPos = tf.position;//get current position at start of update
		
		deltaPos = curPos - prevPos;//get difference in position

		if (applyDelta == true) {
			Subject.GetComponent<Transform>().position += deltaPos;
		}

		
		prevPos = tf.position;//set previous position at end of update

	}

	void OnTriggerStay(Collider other) {
		Subject = other.gameObject;
		applyDelta = true;
	}
	void OnTriggerExit(Collider other) {
		applyDelta = false;
	}
}
