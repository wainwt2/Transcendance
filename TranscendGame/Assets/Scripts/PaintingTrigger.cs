using UnityEngine;
using System.Collections;

public class PaintingTrigger : MonoBehaviour {

	public int pieces;
	private GameObject[] elevParts;
	public static bool paintingIllusion = false;
	public GameObject painting;
	public GameObject musicHandler;

	// Use this for initialization
	void Start () {
		pieces = 0;
		elevParts = GameObject.FindGameObjectsWithTag("elevPart");
		paintingIllusion = false;
		musicHandler = GameObject.FindGameObjectWithTag("musicHandler");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (pieces == 4) {
			if (paintingIllusion) {
				GameObject.FindGameObjectWithTag("corner1").GetComponent<MeshRenderer>().enabled = false;
				GameObject.FindGameObjectWithTag("corner2").GetComponent<MeshRenderer>().enabled = false;
				GameObject.FindGameObjectWithTag("corner3").GetComponent<MeshRenderer>().enabled = false;
				GameObject.FindGameObjectWithTag("corner4").GetComponent<MeshRenderer>().enabled = false;
				GameObject.FindGameObjectWithTag("corner1").GetComponent<Collider>().enabled = false;
				GameObject.FindGameObjectWithTag("corner2").GetComponent<Collider>().enabled = false;
				GameObject.FindGameObjectWithTag("corner3").GetComponent<Collider>().enabled = false;
				GameObject.FindGameObjectWithTag("corner4").GetComponent<Collider>().enabled = false;
				painting.GetComponent<MeshRenderer>().enabled = false;
				painting.GetComponent<Collider>().enabled = false;
				foreach (GameObject elev in elevParts) {
					elev.GetComponent<MeshRenderer>().enabled = true;
				}
			}
			else {
				GameObject.FindGameObjectWithTag("corner1").GetComponent<MeshRenderer>().enabled = true;
				GameObject.FindGameObjectWithTag("corner2").GetComponent<MeshRenderer>().enabled = true;
				GameObject.FindGameObjectWithTag("corner3").GetComponent<MeshRenderer>().enabled = true;
				GameObject.FindGameObjectWithTag("corner4").GetComponent<MeshRenderer>().enabled = true;
				painting.GetComponent<MeshRenderer>().enabled = true;
				foreach (GameObject elev in elevParts) {
					elev.GetComponent<MeshRenderer>().enabled = false;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "gravBody") {
			GameObject piece = other.GetComponentInChildren<PuzzlePiece>().gameObject;
			switch (piece.tag) {
			case "puzzle1":
				GameObject.FindGameObjectWithTag("corner1").GetComponent<MeshRenderer>().enabled = true;
				StartCoroutine(musicHandler.GetComponent<MusicHandler>().FadeSong(2, 60));
				break;
			case "puzzle2":
				GameObject.FindGameObjectWithTag("corner2").GetComponent<MeshRenderer>().enabled = true;
				StartCoroutine(musicHandler.GetComponent<MusicHandler>().FadeSong(3, 60));
				break;
			case "puzzle3":
				GameObject.FindGameObjectWithTag("corner3").GetComponent<MeshRenderer>().enabled = true;
				StartCoroutine(musicHandler.GetComponent<MusicHandler>().FadeSong(4, 60));
				break;
			case "puzzle4":
				GameObject.FindGameObjectWithTag("corner4").GetComponent<MeshRenderer>().enabled = true;
				break;
			default:
				break;
			}
			DestroyObject(other.gameObject);
			pieces++;
		}
	}
}
