using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {

	[SerializeField] Text textScore;
	[SerializeField] GameObject tutorialBtn;
	[SerializeField] GameObject startBtn;
	[SerializeField] GameObject HowtoBtn;

	// Use this for initialization
	void Start () {
		var highScore = SaveController.GetHighScore ();
		textScore.text = highScore.ToString() + "てん";	

		var isTutorialFinished = SaveController.GetIsTutorialFinished();
		if (isTutorialFinished == 0) {
			tutorialBtn.gameObject.SetActive (true);
			startBtn.gameObject.SetActive (false);
			HowtoBtn.gameObject.SetActive (false);
		} else {
			tutorialBtn.gameObject.SetActive (false);
			startBtn.gameObject.SetActive (true);
			HowtoBtn.gameObject.SetActive (true);
		}
			
	}

	public void DeleteSaveData () {
		SaveController.SetHighScore (0);
		SaveController.SetIsTutorialFinished (0);
	}

}
