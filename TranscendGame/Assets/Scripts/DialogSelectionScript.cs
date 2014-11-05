using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogSelectionScript : MonoBehaviour {

	public GameObject [] CanvasArray;

	// Use this for initialization
	void Start () {
		CanvasArray = new GameObject[GetComponent<Transform>().childCount];

		int i = 0;
		foreach(Transform child in GetComponent<Transform>() ) {
			if (child.tag != "notCanvas") {
				CanvasArray[i] = child.gameObject;

				++i;
			}
		}

		foreach (GameObject thing in CanvasArray) {
			Debug.Log(thing.tag);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
