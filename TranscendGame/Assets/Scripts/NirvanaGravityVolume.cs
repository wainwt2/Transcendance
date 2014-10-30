using UnityEngine;
using System.Collections;

public class NirvanaGravityVolume : MonoBehaviour {

	public static bool nirvanaMode;
	private bool buttonHeld;
	private float hAxis;
	private GameObject[] gravBodies;
	private Transform tf;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		nirvanaMode = false;
		buttonHeld = false;
		gravBodies = GameObject.FindGameObjectsWithTag("gravBody");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (nirvanaMode) {
			hAxis = Input.GetAxis("Horizontal");
			if (Mathf.Abs(hAxis) > 0 && !buttonHeld) {
				buttonHeld = true;
				if (hAxis > 0) { // To the right
					tf.Rotate(0f, 0f, 90.0f);
					StartCoroutine(SetGravityOfAll());
				}
				else if (hAxis < 0) { // To the left
					tf.Rotate(0f, 0f, -90.0f);
					StartCoroutine(SetGravityOfAll());
				}
			}
			else if (Mathf.Abs(hAxis) == 0) {
				buttonHeld = false;
			}
		}
	}

	IEnumerator SetGravityOfAll() {
		yield return new WaitForFixedUpdate();
		foreach (GameObject gravBody in gravBodies) {
			GetComponent<AbsoluteGravityDirection>().SetGravity(gravBody);
		}
		GetComponent<AbsoluteGravityDirection>().SetGravity(GameObject.FindGameObjectWithTag("player"));
	}
}
