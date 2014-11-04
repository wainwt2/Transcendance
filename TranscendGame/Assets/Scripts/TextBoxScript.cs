using UnityEngine;
using System.Collections;

public class TextBoxScript : MonoBehaviour {

	public Canvas TextBox;
	public bool ShowCanvas = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (ShowCanvas == true) {
			TextBox.enabled = true;
		}
		else {
			TextBox.enabled = false;
		}
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "player") {
			ShowCanvas = true;
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.tag == "player") {
			ShowCanvas = false;
		}
	}
}
