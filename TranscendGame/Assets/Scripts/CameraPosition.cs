//Author: Dom Cristaldi
//Game: Transcendance

using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

	private Transform tf;
	private MeshRenderer meshRender;

	public int camIndex;

	public bool ShowNodes = false;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		meshRender = GetComponent<MeshRenderer>();
		ShowNodes = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (ShowNodes == false) {
			meshRender.enabled = false;
		}
		else {
			meshRender.enabled = true;
		}
	}
	
}
