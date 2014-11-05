//Author: Dom Cristaldi
//Game: Transcendance

using UnityEngine;
using System.Collections;

public class TeleportScript : MonoBehaviour {

	Transform tf;

	public bool UseDropPosition = true;

	public GameObject DropPosition;
	public GameObject LinkedTeleport;
	
	public bool coolDown;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();

		coolDown = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {

		if (UseDropPosition == true) {
			other.gameObject.GetComponent<Transform>().position = DropPosition.GetComponent<Transform>().position;
			other.gameObject.GetComponent<Transform>().rotation = DropPosition.GetComponent<Transform>().rotation;
		}

		if (UseDropPosition == false) {

			//Debug.Log("Player Original: " + other.GetComponent<Transform>().rotation.eulerAngles);
			coolDown = true;

			if (LinkedTeleport.GetComponent<TeleportScript>().coolDown == false) {

				//find difference in rotation
				Quaternion Transision = LinkedTeleport.GetComponent<Transform>().rotation * Quaternion.Inverse(tf.rotation);

				//find the distance form the teleporter
				Vector3 PositionDiff = tf.position - other.GetComponent<Transform>().position;

				//rotate PositionDiff so it's oriented with the other teleporter
				PositionDiff = Transision * PositionDiff;

				//combine PositionDiff with the LinkedTeleport to get new position
				Vector3 FinalPos = LinkedTeleport.GetComponent<Transform>().position + PositionDiff;

				other.GetComponent<Transform>().position = FinalPos;

				//other.GetComponent<Transform>().position = LinkedTeleport.GetComponent<Transform>().position;
				//other.GetComponent<Transform>().rotation = other.GetComponent<Transform>().rotation * Transision;

				/*
				Transform otherTf = other.GetComponent<Transform>();
				Vector3 TargetVec = other.GetComponent<Transform>().position - tf.position;

				//Quaternion RelativeAngle = Quaternion.Inverse(tf.rotation) * other.GetComponent<Transform>().rotation;
				Quaternion RelativeAngle = otherTf.rotation * Quaternion.Inverse(tf.rotation);

				other.GetComponent<Transform>().position = TargetVec;
				//other.GetComponent<Transform>().rotation = Quaternion.RotateTowards(
				other.GetComponent<Transform>().rotation = otherTf.rotation * RelativeAngle;
				*/
			}
			LinkedTeleport.GetComponent<TeleportScript>().coolDown = true;
		}

	}

	void OnTriggerExit(Collider other) {

		if (UseDropPosition == false) {
			//Debug.Log("Player After: " + other.GetComponent<Transform>().rotation.eulerAngles);
			coolDown = false;
			LinkedTeleport.GetComponent<TeleportScript>().coolDown = false;

		}
	}
}
