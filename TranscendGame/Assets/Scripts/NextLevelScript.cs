using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NextLevelScript : MonoBehaviour {

	public GameObject endCanvas;

	public string NextLevel;
	
	public int FramesTillText = 30;
	public float TimeForFade = 3.0f;

	public float TimeTextPopUp = 2.0f;

	float StartTime;
	float StartFadeTime;

	public List<string> EndText;

	bool FadedIn = false;
	bool PressToAdvance = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (FadedIn == true) {
			if (Time.time - StartFadeTime >= TimeTextPopUp) {
				PressToAdvance = true;
			}
		}
		if (PressToAdvance = true) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				Application.LoadLevel(NextLevel);
			}
		}
	
	}

	void OnTriggerEnter(Collider other) {//begin level ending sequence
		if (other.tag == "player") {
			StartTime = Time.time;
			StartCoroutine(FadeIn(FramesTillText));
		}
	}

	IEnumerator FadeIn (int numFrames) {//for fading in endCanvas
		while (Time.time - StartTime < TimeForFade) {

			endCanvas.GetComponent<CanvasGroup>().alpha = (Time.time - StartTime) / TimeForFade;

			yield return null;
		}
		if (Time.time - StartTime >= TimeForFade) {
			StartFadeTime = Time.time;
			FadedIn = true;
		}

	}
}
