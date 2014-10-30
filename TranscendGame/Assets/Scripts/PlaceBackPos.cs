//Author: Dom Cristaldi
//Game: Transcendance

using UnityEngine;
using System.Collections;

public class PlaceBackPos : MonoBehaviour {

	Transform tf;

	GameObject Pit;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();

		Pit = GameObject.FindGameObjectWithTag("pbPit");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "player") {
			Pit.GetComponent<PlaceBackHandler>().PositionsPath.Add(other.gameObject);
		}
	}
}
