using UnityEngine;
using System.Collections;

public class DynamicGravityDirection : MonoBehaviour {
	
	Transform tf;
	Collider sphereCollider;

	GameObject Player;
	
	public Vector3 GravDirection;
	public float GravScale = 9.8f;
	
	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		sphereCollider = GetComponent<SphereCollider>();

		Player = GameObject.FindGameObjectWithTag("player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GravDirection = tf.position - Player.GetComponent<Transform>().position;
		GravDirection = GravDirection.normalized;
		GravDirection = new Vector3(GravScale * GravDirection.x,
		                            GravScale * GravDirection.y,
		                            GravScale * GravDirection.z);

		//Debug.Log(GravDirection);
	}

}
