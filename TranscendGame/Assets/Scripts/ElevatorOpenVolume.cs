using UnityEngine;
using System.Collections;

public class ElevatorOpenVolume : MonoBehaviour {

	public ElevatorHandler elevator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "player") {
			StartCoroutine(elevator.OpenDoors(30));
		}
	}
}
