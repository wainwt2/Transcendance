using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour {

	private Transform tf;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "interactPhase") {
			Debug.Log("Nirvana Cube found!");
			if (Input.GetKeyDown(KeyCode.Return)) {
				if (tf.parent.GetComponent<PlayerMotor>().canMove) {
					// Phase into the cube
					NirvanaGravityVolume.nirvanaMode = true;
					tf.parent.GetComponent<PlayerMotor>().canMove = false;
					tf.parent.GetComponent<PlayerMotor>().nirvanaLocked = true;
					tf.parent.GetComponent<PlayerMotor>().nirvanaLockTf = other.GetComponent<Transform>();
					tf.parent.GetComponent<Collider>().enabled = false;
					tf.parent.GetComponent<GravityHandler>().enabled = false;
				}
				else {
					// Phase out of the cube
					NirvanaGravityVolume.nirvanaMode = false;
					tf.parent.GetComponent<PlayerMotor>().canMove = true;
					tf.parent.GetComponent<PlayerMotor>().nirvanaLocked = false;
					tf.parent.GetComponent<Transform>().position = other.GetComponent<Transform>().position + new Vector3(0f, 0f, -3.0f);
					tf.parent.GetComponent<Collider>().enabled = true;
					tf.parent.GetComponent<GravityHandler>().enabled = true;
					tf.parent.GetComponent<PlayerMotor>().jumping = true;
					tf.parent.GetComponent<PlayerMotor>().boyAnimator.SetBool("InAir", true);
				}
			}
		}
	}
}
