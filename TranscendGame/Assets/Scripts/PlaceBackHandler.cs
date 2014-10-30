//Author: Dom Cristaldi
//Game: Transcendance

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaceBackHandler : MonoBehaviour {

	public int SetbackAmount = 2;

	public List<GameObject> PositionsPath;
	GameObject Player;

	// Use this for initialization
	void Start () {
		PositionsPath = new List<GameObject>();
		Player = GameObject.FindGameObjectWithTag("player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "player") {
			if (PositionsPath.Count >= SetbackAmount) {//check amount of positions we have

				for (int i = PositionsPath.Count - 1; i > -1; --i) {//if we have enough or more
					if (i == PositionsPath.Count - SetbackAmount) {
						Player.GetComponent<Transform>().position = PositionsPath[i].GetComponent<Transform>().position;
						break;
					}
				}
			}
			else {//if we don't have as many as SetbackAmount
				Player.GetComponent<Transform>().position = PositionsPath[0].GetComponent<Transform>().position;
			}
			List<GameObject> tempList = new List<GameObject>();
			for (int i = 0; i < PositionsPath.Count - SetbackAmount; ++i) {
				tempList.Add(PositionsPath[i]);
			}
			PositionsPath = tempList;
		}
	}
}
