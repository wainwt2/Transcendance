using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour {
	Transform tf;
	Renderer renderer;
	int sign = 1;

	public float speed = 0.5f; 

	public bool invertInitialMovementDirection = false;

	public bool ignoreX = false;
	public bool ignoreY = false;
	public bool ignoreZ = false;
	
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
	public float minZ;
	public float maxZ;

	// Use this for initialization
	void Start () {
		if (invertInitialMovementDirection) {
			sign = -1;		
		}
		tf = GetComponent<Transform>();
		renderer = GetComponent<Renderer>();	
	}
	
	// Update is called once per frame
	void Update () {
		float xTranslation = 0f;
		float yTranslation = 0f;
		float zTranslation = 0f;

		if (!ignoreX) {
			if (tf.position.x > maxX) {
				sign = -1;
			} else if (tf.position.x < minX) {
				sign = 1;
			}
			xTranslation = sign*Time.deltaTime*speed;
		}

		if (!ignoreY) {
			if (tf.position.y > maxY) {
				sign = -1;
			} else if (tf.position.y < minY) {
				sign = 1;
			}
			yTranslation = sign*Time.deltaTime*speed;
		}

		if (!ignoreZ) {
			if (tf.position.z > maxZ) {
				sign = -1;
			} else if (tf.position.z < minZ) {
				sign = 1;
			}
			zTranslation = sign*Time.deltaTime*speed;
		}

		tf.Translate(xTranslation, yTranslation, zTranslation);
	}
}
