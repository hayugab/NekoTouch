using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNekoController : MonoBehaviour {

	[SerializeField] AudioSource audio;

	// Use this for initialization
	void OnMouseUp () {
		audio.Play ();
		iTween.PunchScale (this.gameObject, iTween.Hash (
			"x", 0.3f,
			"y", 0.3f,
			"time", 0.5
		));	
	}
}
