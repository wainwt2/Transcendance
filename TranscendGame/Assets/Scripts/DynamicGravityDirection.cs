using UnityEngine;
using System.Collections;

public class DynamicGravityDirection : MonoBehaviour {
	
	Transform tf;
	Collider sphereCollider;

	GameObject Player;
	
	public Vector3 GravDirection;
	public float GravScale = 1f;
	
	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		sphereCollider = GetComponent<SphereCollider>();
		
		lineRender = GetComponent<LineRenderer>();
		
		Player = GameObject.FindGameObjectWithTag("player");
	}
	
	// Update is called once per frame
	void Update () {
		GravDirection = tf.position - Player.GetComponent<Transform>().position;
		GravDirection = GravDirection.normalized;
		GravDirection = new Vector3(GravScale * GravDirection.x,
		                            GravScale * GravDirection.y,
		                            GravScale * GravDirection.z);
		
		
		Debug.Log(GravDirection);
	}
	
	void debugLines() {
		lineRender.SetPosition(0, Player.GetComponent<Transform>().position);
		lineRender.SetPosition(1, tf.position);
	}
}
