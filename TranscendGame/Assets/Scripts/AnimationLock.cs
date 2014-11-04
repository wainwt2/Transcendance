using UnityEngine;
using System.Collections;

public class AnimationLock : MonoBehaviour {

	private Transform tf;
	public Vector3 lockPos = new Vector3(0f, -0.5f, 0f);

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		tf.localPosition = lockPos;
	}
}
