using UnityEngine;
using System.Collections;

public class WizardLightMovement : MonoBehaviour {

	Transform tf;

	public Light Red;
	public Light Green;
	public Light Blue;

	public float DistMag;

	Vector3 RedPos;
	Vector3 GreenPos;
	Vector3 BluePos;

	float curAngle = 0.0f;
	float deltaAngle = 0.5f;
	float anglePerSecond = 3.0f;

	// Use this for initialization
	void Start () {

		//RedPos = tf.position;
		//GreenPos = tf.position;
		//BluePos = tf.position;
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator MoveLights() {
		while (true) {
			if (curAngle >= 360.0f) {
				curAngle -= 360.0f;
			}

			curAngle += deltaAngle;

			//get the angle for rotations
			Vector2 tempVec = new Vector2(Mathf.Cos(curAngle * Mathf.Deg2Rad),
			                              Mathf.Sin(curAngle * Mathf.Deg2Rad));
			//apply the magnitude
			//tempVec *= 

			//RedPos = new Vector3(


			yield return null;
		}
	}
}
