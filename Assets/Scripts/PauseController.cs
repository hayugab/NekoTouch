using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

	GameObject pauseWindow;

	// Use this for initialization
	void Start () {
		pauseWindow = (GameObject)Resources.Load ("Prefabs/Pause");
	}

	public void SetPause() {
		Time.timeScale = 0f;
		Instantiate (pauseWindow);
		GameDataManager.gameDateManagerIsPause = true;
	}

}
