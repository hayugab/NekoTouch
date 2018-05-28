using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour {

	const string highScoreKey = "HIGH SCORE";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static int GetHighScore () {
		int highScore = PlayerPrefs.GetInt(highScoreKey, 0);
		return highScore;
	}

	public static void SetHighScore (int highScore) {
		PlayerPrefs.SetInt(highScoreKey, highScore);
	}
}
