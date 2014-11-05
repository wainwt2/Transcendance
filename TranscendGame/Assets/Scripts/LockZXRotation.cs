using UnityEngine;
using System.Collections;

public class LockZXRotation : MonoBehaviour {

	private Transform tf;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		tf.rotation = Quaternion.Euler(0f, tf.rotation.eulerAngles.y, 0f);
	}
}
