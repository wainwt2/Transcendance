using UnityEngine;
using System.Collections;

public class LinearCamPosition : MonoBehaviour {
	Transform tf;
	public GameObject Player;

	Vector3 initialPosition;
	Quaternion initialRotation;

	public float distFromPlayer = 4.0f;
	public float elevation = 1.0f;

	public char LockAxis = 'x';

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();

		initialPosition = tf.position;
		initialRotation = tf.rotation;
	}
	
	// Update is called once per frame
	void Update () {

		if (LockAxis == 'x') {
			tf.position = new Vector3(Player.GetComponent<Transform>().position.x + distFromPlayer,
			                          Player.GetComponent<Transform>().position.y + elevation,
			                          Player.GetComponent<Transform>().position.z);
		}
		if (LockAxis == 'y') {
			tf.position = new Vector3(Player.GetComponent<Transform>().position.x,
			                          Player.GetComponent<Transform>().position.y + distFromPlayer,
			                          Player.GetComponent<Transform>().position.z);
		}
		if (LockAxis == 'z') {
			tf.position = new Vector3(Player.GetComponent<Transform>().position.x,
			                          Player.GetComponent<Transform>().position.y,
			                          Player.GetComponent<Transform>().position.z + distFromPlayer);
		}

		tf.LookAt(Player.GetComponent<Transform>().position);

	}
}
