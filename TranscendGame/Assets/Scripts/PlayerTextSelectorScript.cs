using UnityEngine;
using System.Collections;

public class PlayerTextSelectorScript : MonoBehaviour {

	public Canvas MasterCanvas;
	public int indexNum;

	string LookForCanvas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (indexNum != null) {
			if (indexNum == 0) {
				LookForCanvas = "Canvas0";
			}
			if (indexNum == 1) {
				LookForCanvas = "Canvas1";
			}
			if (indexNum == 2) {
				LookForCanvas = "Canvas2";
			}
			if (indexNum == 3) {
				LookForCanvas = "Canvas3";
			}
			if (indexNum == 4) {
				LookForCanvas = "Canvas4";
			}
			if (indexNum == 5) {
				LookForCanvas = "Canvas5";
			}
			if (indexNum == 6) {
				LookForCanvas = "Canvas6";
			}
			if (indexNum == 7) {
				LookForCanvas = "Canvas7";
			}
		}

		foreach (GameObject cnvs in MasterCanvas.GetComponent<DialogSelectionScript>().CanvasArray) {
			//Debug.Log(cnvs.tag);
			if (cnvs != null) {

				if (cnvs.tag == LookForCanvas) {
					cnvs.GetComponent<Canvas>().enabled = true;
				}
				else {

					cnvs.GetComponent<Canvas>().enabled = false;
				}
			}
		}
	}
}
