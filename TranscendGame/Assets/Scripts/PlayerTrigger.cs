using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour {

	private Transform tf;
	public GameObject heldItem;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		heldItem = null;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (heldItem != null) {
			heldItem.GetComponent<Transform>().position = tf.position;
			heldItem.GetComponent<Transform>().rotation = tf.rotation;
		}
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
		else if (other.gameObject.tag == "interactGrab") {
			Debug.Log("Grabbable Object Found!");
			if (Input.GetKeyDown(KeyCode.Return)) {
				if (heldItem == null) {
					heldItem = other.GetComponent<InteractParentHandler>().parent;
					other.GetComponent<InteractParentHandler>().parent.GetComponent<Collider>().enabled = false;
					other.GetComponent<InteractParentHandler>().parent.GetComponent<GravityHandler>().enabled = false;
					GetComponentInParent<PlayerMotor>().boyAnimator.SetTrigger("PickupTrig");
					GetComponentInParent<PlayerMotor>().boyAnimator.SetBool("HasBox", true);
				}
				else if (GetComponentInParent<PlayerMotor>().jumping == false) {
					heldItem = null;
					other.GetComponent<InteractParentHandler>().parent.GetComponent<Collider>().enabled = true;
					other.GetComponent<InteractParentHandler>().parent.GetComponent<GravityHandler>().enabled = true;
					GetComponentInParent<PlayerMotor>().boyAnimator.SetBool("HasBox", false);
				}
			}
		}
	}
}
