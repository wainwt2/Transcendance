using UnityEngine;
using System.Collections;

public class PlayerFlicker : MonoBehaviour {

	MeshRenderer meshRender;

	float randMin = 3.0f;
	float randMax = 7.0f;

	private float changeTime;
	private float StartTime;

	bool isVisible = true;

	// Use this for initialization
	void Start () {

		meshRender = GetComponent<MeshRenderer>();

		updateTime();
	
	}
	
	// Update is called once per frame
	void Update () {

		float AlphaFract = (Time.time - StartTime) / changeTime;

		Color meshColor = meshRender.material.color;

		if (isVisible == true) {
			meshRender.material.color = new Color(meshColor.r, meshColor.g, meshColor.b, 1 - AlphaFract);
		}
		else {
			meshRender.material.color = new Color(meshColor.r, meshColor.g, meshColor.b, AlphaFract);
		}


		if (AlphaFract >= 1.0f) {
			updateTime();//grab new time
			isVisible = !isVisible;//flip the direction we're transitioning the alpha
		}
	}

	void updateTime() {
		changeTime = Random.Range(randMin, randMax);
		StartTime = Time.time;
	}
}
