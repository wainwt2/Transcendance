using UnityEngine;
using System.Collections;

public class CameraVolume : MonoBehaviour {

	Transform tf;
	GameObject CamSystem;
	public GameObject CamPosition;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();

		CamSystem = GameObject.FindGameObjectWithTag("camSystem");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "player") {
			if (CamPosition.tag == "playerCam") {
				CamSystem.GetComponent<CameraSystem>().UsingPlayerCam = true;
			}
			if (CamPosition.tag == "worldCam") {
				CamSystem.GetComponent<CameraSystem>().UsingPlayerCam = false;
				CamSystem.GetComponent<CameraSystem>().WorldCamPosition = CamPosition.GetComponent<CameraPosition>().camIndex;

			}
		}
	}
}
