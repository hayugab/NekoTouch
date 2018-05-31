using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChildrenController : MonoBehaviour {

	[SerializeField] TutorialController parent;
	[SerializeField] GameObject nextBtn;

	// Use this for initialization
	void Start () {

	}

	public void Init () {
		nextBtn.SetActive (false);
		Debug.Log ("Start");
		StartCoroutine (WaitShowBtn());
	}

	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator WaitShowBtn () {
		yield return new WaitForSeconds (1.0f);
		nextBtn.gameObject.SetActive (true);
		Debug.Log ("WaitShowBtn");
	}

	public void TapToNext () {
		parent.Next ();
	}
}
