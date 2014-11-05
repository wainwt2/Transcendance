using UnityEngine;
using System.Collections;

public class PaintingBoolSwitcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "player") {
			PaintingTrigger.paintingIllusion = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "player") {
			PaintingTrigger.paintingIllusion = false;
		}
	}
}
