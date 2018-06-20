using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System;

public class MainController : MonoBehaviour {
	
//	[SerializeField] GameObject[] Nekos;
	[SerializeField] GameObject objField;
	[SerializeField] Text textScore;
	[SerializeField] int nekoNum;
	[SerializeField] BoxCollider2D filter;
	[SerializeField] GameObject objGameOver;
	[SerializeField] AudioSource btnSound;
	[SerializeField] AudioSource mainBGM;
	[SerializeField] AudioSource gameoverBGM;
	[SerializeField] GameObject objHighScore;
	[SerializeField] Text objGameOverScore;
	[SerializeField] GameObject objScore;
	[SerializeField] GameObject adsBtn;
	[SerializeField] GameObject retryBtn;
	[SerializeField] GameObject titleBtn;
	[SerializeField] float downFilterSpeed;
	[SerializeField] float[] filterSpeedList;
	[SerializeField] int[] filterScoreList;

	List<NekoController> nekoList = new List<NekoController> ();

	GameObject nekoPrefab;
	int score = 0;
	int prevScore = 0;
	float filterOffsetY;
	bool down = false;
	bool speedUp = false;
	bool isGameOver = false;
	bool isPause = false;
	int point = 0;
	int timeCount = 0;
	float filterSpeed;
	int playCount = 0;
	int filterSpeedIndex = 0;


	const int TIME_COUNT_MAX = 10;

	enum SpeedMode {
		One,
		Two,
		Three,
		Four,
		Five,
		Six,
		Seven
	}

	SpeedMode speedMode = SpeedMode.One;

	public bool IsGameOver {
		get { 
			return isGameOver;
		}
	}

	// Use this for initialization
	void Start () {

		if (nekoPrefab == null)
			return;

		Init ();
	}

	void Init () {
		filterOffsetY = -1f;
		point = 0;
		score = 0;
		prevScore = 0;
		filterSpeedIndex = 0;
		SetFilterSpeed (filterSpeedIndex);
		isGameOver = false;
		objScore.gameObject.SetActive (true);
		textScore.text = score.ToString ();
		objGameOver.SetActive (false);
		gameoverBGM.Stop ();
		mainBGM.Play ();

		foreach(Transform child in objField.gameObject.transform){
			Destroy(child.gameObject);
		}

		for (int i = 0; i < nekoNum; i++) {
			var nekoObj = Instantiate (nekoPrefab, objField.gameObject.transform);
			var neko = nekoObj.GetComponent<NekoController> ();
			neko.Init (this);
		}		
	}

	public void AdsShow() {
		adsBtn.gameObject.SetActive (false);
		retryBtn.gameObject.SetActive (true);
	}

	public void RetryInit() {
		btnSound.Play ();
		Init ();
	}
		
	void Awake () {
		LoadResouces ();
	}

	void LoadResouces () {
		nekoPrefab = (GameObject)Resources.Load ("Prefabs/Neko");
	}

	public void DestroyNeko(NekoController neko) {
		nekoList.Remove (neko);
		Destroy (neko.gameObject);
	}

	public void AddNeko () {
		var nekoObj = Instantiate (nekoPrefab, objField.gameObject.transform);
		var neko = nekoObj.GetComponent<NekoController> ();
		neko.Init (this);
		nekoList.Add (neko);
	}

	public void ShareNeko () {
		StartCoroutine (ShareTwitter());
	}

	IEnumerator ShareTwitter () {
		string fileName = System.DateTime.Now.ToString("ScreenShot yyyy-MM-dd HH.mm.ss") + ".png";
		string imagePath = Application.persistentDataPath + "/" + fileName;

		ScreenCapture.CaptureScreenshot (fileName);

		yield return new WaitForEndOfFrame ();

		string text = "ねこをたくさんわけたよ！ #ねこわける";
		yield return new WaitForSeconds (1);

		SocialConnector.SocialConnector.Share(text, "", imagePath);
	}

	public void SetPoint (int point) {
		prevScore = score;
		this.point = point;
		score += point * 100;
		textScore.text = score.ToString ();
		down = true;
		timeCount = 0;
	}

	public void CheckFilterSpeed () {
		for (int i = 1; i < filterScoreList.Length; i++) {
			var filterscore = filterScoreList [i];
			if (filterscore > score)
				break;

			if (prevScore < filterscore && score >= filterscore) {
				filterSpeedIndex = i;
				SetFilterSpeed (filterSpeedIndex);
				break;
			}
		}
	}

	void ShowAdsBtn() {
		adsBtn.gameObject.SetActive (true);
		retryBtn.gameObject.SetActive (false);
	}

	public void ShowRetryBtn () {
		adsBtn.gameObject.SetActive (false);
		retryBtn.gameObject.SetActive (true);
	}

	void InitGameOver () {
		objGameOver.SetActive (true);
		isGameOver = true;
		mainBGM.Stop ();
		gameoverBGM.Play ();
		objGameOverScore.text = score.ToString ();
		objScore.gameObject.SetActive (false);
		bool isHighScore = score > SaveController.GetHighScore ();
		if (isHighScore) {
			SaveController.SetHighScore (score);
			StartCoroutine (SendHighScore (score));
		}
		objHighScore.SetActive (isHighScore);
		if (playCount >= 2) {
			playCount = 0;
			ShowAdsBtn ();
			return;
		}
		playCount++;
		ShowRetryBtn ();
	}

	IEnumerator SendHighScore (int score) {
		NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject> ("HighScore");
		var uuid = SaveController.GetUuid ();

		query.WhereEqualTo("Uuid",uuid);
		query.FindAsync ((List<NCMBObject> objList ,NCMBException e) => {
			if (e != null) {
				NCMBObject obj = new NCMBObject("HighScore");
				obj ["Name"] = SaveController.GetName ();
				obj["Score"] = SaveController.GetHighScore();
				obj ["Uuid"] = SaveController.GetUuid ();
				obj.SaveAsync();
			} else {
				foreach (NCMBObject obj in objList) {
					Debug.Log ("objectId:" + obj.ObjectId);
					obj["Score"] = SaveController.GetHighScore();
					obj.SaveAsync();
				}
			}
		});
		yield return null;
	}

		
	void SetFilterSpeed(int index) {
		if (filterSpeedList.Length <= index)
			return;
		filterSpeed = filterSpeedList [index];
	}

	public void SetPause (bool isPause) {
		this.isPause = isPause;
	}

	void Update () {
		if (isGameOver)
			return;

		if (GameDataManager.gameDateManagerIsPause)
			return;

		if (filterOffsetY > 1.5f) {
			InitGameOver ();
			return;
		}

		if (down) {	
			filterOffsetY -= downFilterSpeed * point;		

			timeCount++;
			if (TIME_COUNT_MAX == timeCount)
				down = false;
		} else {
			filterOffsetY += filterSpeed;
		}

		filter.transform.position = new Vector3 (filter.transform.position.x, filterOffsetY, filter.transform.position.z);
	}
}
