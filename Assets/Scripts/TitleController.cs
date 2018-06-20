using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {

	[SerializeField] Text textScore;
	[SerializeField] GameObject tutorialBtn;
	[SerializeField] GameObject startBtn;
	[SerializeField] GameObject HowtoBtn;
	[SerializeField] GameObject rankingBtn;
	[SerializeField] SceneLoadController sceneLoadController;

	// Use this for initialization
	void Start () {
		var highScore = SaveController.GetHighScore ();
		textScore.text = highScore.ToString() + "てん";	

		var isTutorialFinished = SaveController.GetIsTutorialFinished();
		if (isTutorialFinished == 0 || SaveController.GetName() == string.Empty) {
			tutorialBtn.gameObject.SetActive (true);
			startBtn.gameObject.SetActive (false);
			HowtoBtn.gameObject.SetActive (false);
			rankingBtn.gameObject.SetActive (false);
		} else {
			tutorialBtn.gameObject.SetActive (false);
			startBtn.gameObject.SetActive (true);
			HowtoBtn.gameObject.SetActive (true);
			rankingBtn.gameObject.SetActive (true);
		}
			
	}

	public void DeleteSaveData () {
		SaveController.SetHighScore (0);
		SaveController.SetIsTutorialFinished (0);
		SaveController.ResetUserData ();
	}

	public void DeleteSaveNameData () {
		SaveController.ResetUserData ();
	}

	public void ChangeSceneMain () {
		if (SaveController.GetName() == string.Empty)
			sceneLoadController.SceneLoadInputName ();
		else 
			sceneLoadController.SceneLoadMain ();
	}

	public void ChangeSceneTutorial () {
		if (SaveController.GetName() == string.Empty)
			sceneLoadController.SceneLoadInputName ();
		else 
			sceneLoadController.SceneLoadTutorial ();
	}

	public void ChangeSceneRanking () {
		if (SaveController.GetName() == string.Empty)
			sceneLoadController.SceneLoadInputName ();
		else 
			sceneLoadController.SceneLoadRanking ();
	}


}
