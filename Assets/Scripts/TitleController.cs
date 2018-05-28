using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {

	[SerializeField] Text textScore;

	// Use this for initialization
	void Start () {
		var highScore = SaveController.GetHighScore ();
		textScore.text = highScore.ToString() + "てん";
	}
}
