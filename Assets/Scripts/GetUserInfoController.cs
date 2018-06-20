using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System;

public class GetUserInfoController : MonoBehaviour {

	[SerializeField] Text InputName;
	[SerializeField] GameObject confirmPopupRoot;
	[SerializeField] SceneLoadController sceneController;
	GameObject confirmPopup;

	// Use this for initialization
	void Start () {
		confirmPopup = (GameObject)Resources.Load ("Prefabs/NameConfirmPopup");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowConfirmPopup() {
		var confirmObj = Instantiate (confirmPopup, confirmPopupRoot.transform);
		var confirm = confirmObj.GetComponent<GetUserInfoConfirmController> ();
		Debug.Log (confirm);
		var name = InputName.text;
		if (name == string.Empty)
			name = "名無しの猫";
		confirm.Init (name, SetName);
	}

	void SetName() {
		SaveController.SetName (InputName.text);
		SaveController.CreateUuid ();

		NCMBObject obj = new NCMBObject("HighScore");
		obj["Name"]  = InputName.text;
		obj["Score"] = SaveController.GetHighScore();
		obj ["Uuid"] = SaveController.GetUuid ();
		obj.SaveAsync();


		sceneController.SceneLoadTutorial ();
	}
}
