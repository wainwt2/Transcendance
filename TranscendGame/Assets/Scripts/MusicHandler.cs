using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicHandler : MonoBehaviour {
	
	public int currentSong;
	public GameObject[] songs;

	// Use this for initialization
	void Start () {
		currentSong = 1;
		songs = GameObject.FindGameObjectsWithTag("musicSource");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator FadeSong(int n, int steps) {
		GameObject oldSong = null;
		GameObject newSong = null;
		foreach (GameObject song in songs) {
			if (song.GetComponent<MusicIndexer>().songIndex == currentSong) {
				oldSong = song;
				Debug.Log ("Old Song Found.");
			}
			if (song.GetComponent<MusicIndexer>().songIndex == n) {
				newSong = song;
				Debug.Log ("New Song Found.");
			}
		}
		for (int i = 0; i < steps; i++) {
			if (oldSong != null && newSong != null) {
				oldSong.GetComponent<AudioSource>().volume -= (1f/(float)steps);
				newSong.GetComponent<AudioSource>().volume += (1f/(float)steps);
				Debug.Log("Fading...");
				yield return new WaitForFixedUpdate();
			}
		}
		currentSong = n;
	}
}
