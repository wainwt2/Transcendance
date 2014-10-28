//Author: Dom Cristaldi
//Game: Transcendance

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSystem : MonoBehaviour {

	public GameObject ViewCam;

	GameObject PlayerCamera;
	List<GameObject> WorldCameraList = new List<GameObject>();

	GameObject SelectedCam;

	public bool UsingPlayerCam = true;
	public int WorldCamPosition;

	// Use this for initialization
	void Start () {

		ViewCam = GameObject.FindGameObjectWithTag("MainCamera");//grab the Main Camera as the default camera

		PlayerCamera = GameObject.FindGameObjectWithTag("playerCam");//make the playerCamera it's own separate reference

		GameObject [] tempObjectList = GameObject.FindGameObjectsWithTag("worldCam");//get a list of all worldCameras
		foreach (GameObject position in tempObjectList) {//add worldCameras to the CameraList
			WorldCameraList.Add(position);
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		if (UsingPlayerCam == true) {
			UsePlayerCam();
		}
		else {
			UseWorldCam(WorldCamPosition);
		}
	
	}

	void UsePlayerCam() {//operations when using the playerCam
		ViewCam.GetComponent<Transform>().position = PlayerCamera.GetComponent<Transform>().position;
		ViewCam.GetComponent<Transform>().rotation = PlayerCamera.GetComponent<Transform>().rotation;

	}

	void UseWorldCam(int camIndex) {//operations when using the worldCams
		ViewCam.GetComponent<Transform>().position = WorldCameraList[camIndex].GetComponent<Transform>().position;
		ViewCam.GetComponent<Transform>().rotation = WorldCameraList[camIndex].GetComponent<Transform>().rotation;
	}
}
