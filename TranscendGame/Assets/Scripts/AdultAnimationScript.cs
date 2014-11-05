using UnityEngine;
using System.Collections;

public class AdultAnimationScript : MonoBehaviour {

	Transform tf;
	Animator anim;

	SkinnedMeshRenderer skinMeshRender;

	GameObject Player;

	public GameObject SpeechTrigger;

	public bool ForwardTrigger = false;
	public bool SittingTrigger = false;
	public bool DownTrigger = false;
	public bool UpTrigger = false;
	public bool WinceTrigger = false;

	public bool IsTalking = false;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		anim = GetComponent<Animator>();

		skinMeshRender.enabled = false;

		Player = GameObject.FindGameObjectWithTag("player");
	}
	
	// Update is called once per frame
	void Update () {

		if (SpeechTrigger != null) {//toggle NPC's talking animation
			if (SpeechTrigger.GetComponent<TextBoxScript>().ShowCanvas == true) {
				IsTalking = true;
				tf.LookAt(Player.GetComponent<Transform>().position);
			}
			else {
				IsTalking = false;
			}
		}

		if (ForwardTrigger == true) {
			anim.SetTrigger("ForwardT");
		}
		if (SittingTrigger == true) {
			anim.SetTrigger("SittingT");
		}
		if (DownTrigger == true) {
			anim.SetTrigger("DownT");
		}
		if (UpTrigger == true) {
			anim.SetTrigger("UpT");
		}
		if (WinceTrigger == true) {
			anim.SetTrigger("WinceT");
		}
		anim.SetBool("TalkBool", IsTalking);
	
	}
}
