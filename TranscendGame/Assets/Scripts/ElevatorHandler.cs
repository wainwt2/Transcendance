using UnityEngine;
using System.Collections;

public class ElevatorHandler : MonoBehaviour {

	public GameObject door1;
	public GameObject door2;
	private Vector3 door1Slide;
	private Vector3 door2Slide;
	private Transform door1tf;
	private Transform door2tf;
	private Transform tf;
	public float upwardSpeed;
	public static bool doorsUnlocked;
	public static bool doorsOpen;

	// Use this for initialization
	void Start () {
		doorsUnlocked = false;
		doorsOpen = false;
		door1tf = door1.GetComponent<Transform>();
		door2tf = door2.GetComponent<Transform>();
		tf = GetComponent<Transform>();
		door1Slide = new Vector3(1.0f, 0f, 0f);
		door2Slide = new Vector3(2.0f, 0f, 0f);
		door1Slide = tf.rotation * door1Slide;
		door2Slide = tf.rotation * door2Slide;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown(KeyCode.Return)) {
			Debug.Log ("Opening doors.");
			StartCoroutine(OpenDoors(30));
		}
		if (Input.GetKeyDown(KeyCode.Backspace)) {
			Debug.Log ("Closing doors.");
			StartCoroutine(CloseDoors(30, false));
		}
		if (Input.GetKeyDown(KeyCode.BackQuote)) {
			Debug.Log ("Moving elevator.");
			StartCoroutine(MoveUpwards());
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "player") {
			other.GetComponent<PlayerMotor>().canMove = false;
			StartCoroutine(CloseDoors(30, true));
		}
	}

	IEnumerator OpenDoors(int frames) {
		if (!doorsOpen) {
			doorsOpen = true;
			for (int i = 0; i < frames; i++) {
				door1tf.position += (door1Slide/(float)frames);
				door2tf.position += (door2Slide/(float)frames);
				yield return new WaitForFixedUpdate();
			}
		}
	}

	IEnumerator CloseDoors(int frames, bool moveUpwardOnFinish) {
		if (doorsOpen) {
			doorsOpen = false;
			for (int i = 0; i < frames; i++) {
				door1tf.position -= (door1Slide/(float)frames);
				door2tf.position -= (door2Slide/(float)frames);
				yield return new WaitForFixedUpdate();
			}
			if (moveUpwardOnFinish) {
				StartCoroutine(MoveUpwards());
			}
		}
	}

	IEnumerator MoveUpwards() {
		while (true) {
			Vector3 dPos = tf.rotation * new Vector3(0f, upwardSpeed, 0f);
			tf.position += dPos;
			yield return new WaitForFixedUpdate();
		}
	}
}
