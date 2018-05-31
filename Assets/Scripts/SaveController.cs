using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour {

	const string highScoreKey = "HIGH SCORE";
	const string tutorialFinishedKey = "TUTORIAL FINISH";

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

	public static int GetIsTutorialFinished () {
		int isTutorialFinished = PlayerPrefs.GetInt (tutorialFinishedKey, 0);
		return isTutorialFinished;
	}

	public static void SetIsTutorialFinished (int isTutorial) {
		PlayerPrefs.SetInt (tutorialFinishedKey, isTutorial);
	}
}
