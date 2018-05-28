using UnityEngine;
using System.Collections;

public class SceneLoadController : MonoBehaviour {

	public void SceneLoadMain (){
		Application.LoadLevel ("main");
	}

	public void SceneLoadTitle (){
		StartCoroutine (GoToTitleScene());
	}

	IEnumerator GoToTitleScene() {
		SaveController.SetHighScore (0);
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel ("title");
	}
}
