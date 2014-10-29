//Author: Dom Cristaldi
//Game: Transcendance

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSystem : MonoBehaviour {

	public GameObject ViewCam;
	public float TravelTime = 0.5f;
	float StartTime;

	GameObject PlayerCamera;
	List<GameObject> WorldCameraList = new List<GameObject>();

	GameObject SelectedCam;

	public bool UsingInterpolation = true;	//for Camera interpolation
	Vector3 StartPos;
	Vector3 EndPos;
	Quaternion StartRot;
	Quaternion EndRot;

	bool isMoving;

	public bool UsingPlayerCam = true;
	public int WorldCamPosition;
	

	// Use this for initialization
	void Start () {

		ViewCam = GameObject.FindGameObjectWithTag("MainCamera");//grab the Main Camera as the default camera
		StartTime = Time.time;

		PlayerCamera = GameObject.FindGameObjectWithTag("playerCam");//make the playerCamera it's own separate reference

		GameObject [] tempObjectList = GameObject.FindGameObjectsWithTag("worldCam");//get a list of all worldCameras
		foreach (GameObject position in tempObjectList) {//add worldCameras to the CameraList
			WorldCameraList.Add(position);
		}
		for (int i = 0; i < WorldCameraList.Count; ++i) {
			WorldCameraList[i].GetComponent<CameraPosition>().camIndex = i;
		}

		AssignSelectedCam();	//initialize at the camera we want to use

		StartPos = ViewCam.GetComponent<Transform>().position;	//used for Interpolation between camera positoins
		StartRot = ViewCam.GetComponent<Transform>().rotation;

		EndPos = SelectedCam.GetComponent<Transform>().position;
		EndRot = SelectedCam.GetComponent<Transform>().rotation;

		isMoving = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (StartPos == EndPos) {//check if we must move the camera
			isMoving = false;
			StartTime = Time.time;
		}
		else {
			isMoving = true;
		}

		AssignSelectedCam();	//grab the cam we want to be at

		if (isMoving == false) {//if stationary, store position
			StartPos = ViewCam.GetComponent<Transform>().position;
			StartRot = ViewCam.GetComponent<Transform>().rotation;

			StartTime = Time.time;
		}

		EndPos = SelectedCam.GetComponent<Transform>().position;	//grab destination
		EndRot = SelectedCam.GetComponent<Transform>().rotation;

		if (isMoving == true) {//interpolate between positions if we are moving
			if (UsingInterpolation == true) {
				MoveWithLerp();
			}
			else {
				MoveWithoutLerp();
			}
		}
	}

	void AssignSelectedCam() {
		if (UsingPlayerCam == true) {
			SelectedCam = PlayerCamera;
		}
		else {
			SelectedCam = WorldCameraList[WorldCamPosition];
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

	void MoveWithLerp() {
		float fract = (Time.time - StartTime) / TravelTime;
		Vector3 TransPos = Vector3.Lerp(StartPos, EndPos, fract);
		Quaternion TransRot = Quaternion.Slerp(StartRot, EndRot, fract);

		ViewCam.GetComponent<Transform>().position = TransPos;
		ViewCam.GetComponent<Transform>().rotation = TransRot;


		if (fract >= 1.0f && UsingPlayerCam == false) {//Jerry-rigged fix
			StartPos = EndPos;
			StartRot = EndRot;
		}

	}

	void MoveWithoutLerp() {
		ViewCam.GetComponent<Transform>().position = SelectedCam.GetComponent<Transform>().position;
		ViewCam.GetComponent<Transform>().rotation = SelectedCam.GetComponent<Transform>().rotation;
	}

}
