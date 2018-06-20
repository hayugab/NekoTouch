using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour {

	const string highScoreKey = "HIGH SCORE";
	const string tutorialFinishedKey = "TUTORIAL FINISH";
	const string uuidKey = "UUID UNITY";
	const string userNameKey = "USER NAME";

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

	public static string GetUuid () {
		string uuid = PlayerPrefs.GetString (uuidKey, string.Empty);
		if (uuid == string.Empty) {
			uuid = CreateUuid ();
			SetCreateUUid (uuid);
		}
		return uuid;
	}

	public static string CreateUuid() {
		System.Guid guid = System.Guid.NewGuid ();
		var uuid =  guid.ToString ();
		return uuid;
	}

	public static void SetCreateUUid(string uuid) {
		PlayerPrefs.SetString (uuidKey, uuid);
	}

	public static string GetName() {
		string name = PlayerPrefs.GetString (userNameKey, "名無しの猫");
		return name;
	}

	public static void SetName(string name) {
		if (name == string.Empty)
			name = "名無しの猫";
		PlayerPrefs.SetString (userNameKey, name);
	}

	public static void ResetUserData () {
		PlayerPrefs.SetString (uuidKey, string.Empty);
		PlayerPrefs.SetString (userNameKey, string.Empty);
	}
}
