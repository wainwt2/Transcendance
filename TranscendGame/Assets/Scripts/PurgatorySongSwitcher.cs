using UnityEngine;
using System.Collections;

public class PurgatorySongSwitcher : MonoBehaviour {

	private GameObject musicHandler;

	// Use this for initialization
	void Start () {
		musicHandler = GameObject.FindGameObjectWithTag("musicHandler");
		StartCoroutine(SongTimings());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator SongTimings() {
		for (int i = 0; i < 4000; i++) {
			yield return new WaitForFixedUpdate();
		}
		StartCoroutine(musicHandler.GetComponent<MusicHandler>().FadeSong(2, 60));
		for (int i = 0; i < 4000; i++) {
			yield return new WaitForFixedUpdate();
		}
		StartCoroutine(musicHandler.GetComponent<MusicHandler>().FadeSong(3, 60));
		for (int i = 0; i < 4000; i++) {
			yield return new WaitForFixedUpdate();
		}
		StartCoroutine(musicHandler.GetComponent<MusicHandler>().FadeSong(4, 60));
		for (int i = 0; i < 4000; i++) {
			yield return new WaitForFixedUpdate();
		}
		StartCoroutine(musicHandler.GetComponent<MusicHandler>().FadeSong(5, 60));
	}
}
