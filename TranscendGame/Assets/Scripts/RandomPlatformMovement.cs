using UnityEngine;
using System.Collections;

public class RandomPlatformMovement : MonoBehaviour {
	Transform tf;
	Renderer renderer;

	float speed = 5f; 

	int signX;
	int signZ;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		renderer = GetComponent<Renderer>();

		if (Random.Range (-1, 1) < 0) {
			signX = -1;
		} else {
			signX = 1;
		}

		if (Random.Range (-1, 1) < 0) {
			signZ = -1;
		} else {
			signZ = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (tf.position.x < 36.2) {
			signX = 1;
		}
		
		if (tf.position.x > 56.2) {
			signX = -1;
		}

		if (tf.position.z < -19.25) {
			signZ = 1;
		}
		
		if (tf.position.z > 20.75) {
			signZ = -1;
		}

		tf.Translate(signX*Time.deltaTime*speed, 0f, signZ*Time.deltaTime*speed);
	}

	void OnTriggerEnter (Collider other) {
		invertSignX();
		invertSignZ();
	}

	void invertSignX() {
		if (signX == 1) {
			signX = -1;
		} else {
			signX = 1;
		}
	}

	void invertSignZ() {
		if (signZ == 1) {
			signZ = -1;
		} else {
			signZ = 1;
		}
	}
}
