using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System;

public class SendRankingController : MonoBehaviour {
	[SerializeField] Text labelName;
	[SerializeField] Text labelScore;


	string name;
	int score;

	// Use this for initialization
	void Start () {
		name = SaveController.GetName ();
		score = SaveController.GetHighScore ();
		labelName.text = name + "さんの\nハイスコア";
		labelScore.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SendRankingData () {
		NCMBObject obj = new NCMBObject("HighScore");
		obj["Name"]  = name;
		obj["Score"] = score;
		obj ["Uuid"] = SaveController.GetUuid ();
		obj.SaveAsync();
	}
}
