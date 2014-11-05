using UnityEngine;
using System.Collections;

public class ElevatorIllusionKeeper : MonoBehaviour {

	private bool alwaysOn;

	// Use this for initialization
	void Start () {
		alwaysOn = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (alwaysOn) {
			PaintingTrigger.paintingIllusion = true;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "player") {
			alwaysOn = true;
		}
	}
}
