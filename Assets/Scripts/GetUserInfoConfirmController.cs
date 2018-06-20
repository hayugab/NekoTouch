using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetUserInfoConfirmController : MonoBehaviour {

	[SerializeField] Text nameText;
	System.Action btnYesAction;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init (string name, System.Action yesAction) {
		nameText.text = name;
		btnYesAction = yesAction;
	}

	public void doYes () {
		if (btnYesAction != null)
		btnYesAction();
		Destroy (this.gameObject);
	}

	public void doNo () {
		Destroy (this.gameObject);
	}
}
