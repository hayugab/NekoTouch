using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingChildController : MonoBehaviour {

	[SerializeField] Text labelRank;
	[SerializeField] Text labelName;
	[SerializeField] Text labelSrore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init (int rank, string name, int score) {
		labelRank.text = rank.ToString() + "位";
		labelName.text = name;
		labelSrore.text = score.ToString (); 
	}
}
