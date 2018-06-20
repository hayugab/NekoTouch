using NCMB;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RankingMainController : MonoBehaviour {

//	[SerializeField] Text rankingText;
	[SerializeField] GameObject rankingListRoot;
	[SerializeField] Text rankingText;
	[SerializeField] Text nameText;
	[SerializeField] Text scoreText;
	GameObject rankingChildPrefab;
	List<HighScore> highScoreList = new List<HighScore>();
	const int RANKING_LIMIT = 20;


	class HighScore {
		public int score;
		public string username;
	}

	// Use this for initialization
	void Start () {
		rankingText.text = "通信中";
		nameText.text = SaveController.GetName () + "さんのハイスコア";
		scoreText.text = SaveController.GetHighScore ().ToString ();
		StartCoroutine (fetchTopRankers());
		rankingChildPrefab = (GameObject)Resources.Load ("Prefabs/RankingChild");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SendRankingData () {
//		var name = inputName.text;
//		var score = inputScore.text;
//
//		NCMBObject obj = new NCMBObject("HighScore");
//		obj["Name"]  = name;
//		obj["Score"] = score;
//		obj ["Uuid"] = SaveController.GetUuid ();
//		obj.SaveAsync();
	}


	// サーバーからトップ20を取得 ---------------    
	IEnumerator fetchTopRankers()
	{
		// データストアの「HighScore」クラスから検索
		NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject> ("HighScore");
		query.OrderByDescending ("Score");
		query.Limit = RANKING_LIMIT;
		query.FindAsync ((List<NCMBObject> objList ,NCMBException e) => {

			if (e != null) {
				//検索失敗時の処理
//				rankingText.text = "ランキング集計中";
			} else {
				//検索成功時の処理
				// 取得したレコードをHighScoreクラスとして保存
				foreach (NCMBObject obj in objList) {
					int    s = System.Convert.ToInt32(obj["Score"]);
					string n = System.Convert.ToString(obj["Name"]);
					HighScore h = new HighScore();
					h.username = n;
					h.score = s;
					highScoreList.Add(h);
				}


				if (highScoreList.Count == 0) {
					rankingText.text = "ランキング集計中";
				}
				else {
					rankingText.gameObject.SetActive(false);
					int rank = 1;
					foreach (var target in highScoreList) {
						var obj = Instantiate (rankingChildPrefab, rankingListRoot.transform);
						var rankingchild = obj.GetComponent<RankingChildController>();
						rankingchild.Init (rank, target.username, target.score);
						rank++;
					}					
				}
			}
		});

		yield return null;
	}

	public void ResetName () {
		SaveController.ResetUserData ();
	}
}
