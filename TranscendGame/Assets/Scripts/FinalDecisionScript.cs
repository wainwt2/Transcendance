using UnityEngine;
using System.Collections;

public class FinalDecisionScript : MonoBehaviour {

	public Material mat;
	public GameObject cnvs;
	public GameObject MatLocation;

	public GameObject otherVolume;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "player") {
			MatLocation.GetComponent<SkinnedMeshRenderer>().material = mat;
			cnvs.GetComponent<Canvas>().enabled = true;
			otherVolume.GetComponent<BoxCollider>().enabled = false;
		}
		                        
	}
}
