using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

	[SerializeField] SceneLoadController sceneLoadController;
	[SerializeField] GameObject[] TutorialList;

	int index = 0;

	// Use this for initialization
	void Start () {
		for (var i = 0; i < TutorialList.Length; i++) {
			TutorialList [i].SetActive (false);
		}

		TutorialList [index].SetActive (true);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Next () {
		var currentIndex = index;
		index++;
		if (index >= TutorialList.Length) {
			SaveController.SetIsTutorialFinished (1);
			sceneLoadController.SceneLoadMain ();
			return;
		}
		TutorialList [currentIndex].SetActive (false);
		TutorialList [index].SetActive (true);
		TutorialList [index].GetComponent<TutorialChildrenController> ().Init ();
	}
}
