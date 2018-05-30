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
		yield return new WaitForSeconds(1.0f);
		Application.LoadLevel ("title");
	}
}
