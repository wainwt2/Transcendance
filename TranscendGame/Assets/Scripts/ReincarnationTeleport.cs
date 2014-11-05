using UnityEngine;
using System.Collections;

public class ReincarnationTeleport : MonoBehaviour {

	public GameObject otherTeleport;
	private Transform tf;
	private Transform otherTf;
	public Vector3 originOffset;
	public float scaleFactor = 1;
	public int songIndex;
	public GameObject musicHandler;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		otherTf = otherTeleport.GetComponent<Transform>();
		musicHandler = GameObject.FindGameObjectWithTag("musicHandler");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "player") {
			other.GetComponent<Transform>().position = otherTf.position + originOffset;
			other.GetComponent<PlayerMotor>().RescalePlayer(scaleFactor);
			StartCoroutine(musicHandler.GetComponent<MusicHandler>().FadeSong(songIndex, 60));
		}
	}
}
