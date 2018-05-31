using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainController : MonoBehaviour {
	
//	[SerializeField] GameObject[] Nekos;
	[SerializeField] GameObject objField;
	[SerializeField] Text textScore;
	[SerializeField] int nekoNum;
	[SerializeField] BoxCollider2D filter;
	[SerializeField] GameObject objGameOver;
	[SerializeField] float startfilterSpeed;
	[SerializeField] AudioSource btnSound;
	[SerializeField] AudioSource mainBGM;
	[SerializeField] AudioSource gameoverBGM;
	[SerializeField] GameObject objHighScore;
	[SerializeField] Text objGameOverScore;
	[SerializeField] GameObject objScore;
	[SerializeField] GameObject adsBtn;
	[SerializeField] GameObject retryBtn;
	[SerializeField] GameObject titleBtn;

	List<NekoController> nekoList = new List<NekoController> ();

	GameObject nekoPrefab;
	int score = 0;
	float filterOffsetY;
	bool down = false;
	bool speedUp = false;
	bool isGameOver = false;
	int point = 0;
	int timeCount = 0;
	float filterSpeed;
	int playCount = 0;


	const int TIME_COUNT_MAX = 10;

	enum SpeedMode {
		Slow,
		Middle,
		High,
		Special
	}

	SpeedMode speedMode = SpeedMode.Slow;

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
		filterSpeed = startfilterSpeed;
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
//			nekoList.Add (neko);
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
//
//	public void DestroyAllNeko() {
//		foreach (var neko in nekoList) {
//			nekoList.Remove (neko);
//			Destroy (neko.gameObject);
//		}
//		Init ();
//	}
//
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

		string text = "ねこをたくさんわけたよ！ #テスト";
		yield return new WaitForSeconds (1);

		SocialConnector.SocialConnector.Share(text, "url", imagePath);
	}

	public void SetPoint (int point) {
		this.point = point;
		score += point * 5;
		textScore.text = score.ToString ();
		down = true;
		timeCount = 0;
	}

	void ShowAdsBtn() {
		adsBtn.gameObject.SetActive (true);
		retryBtn.gameObject.SetActive (false);
//		titleBtn.gameObject.SetActive (false);
	}

	public void ShowRetryBtn () {
		adsBtn.gameObject.SetActive (false);
		retryBtn.gameObject.SetActive (true);
//		titleBtn.gameObject.SetActive (true);
	}

	void InitGameOver () {
		objGameOver.SetActive (true);
		isGameOver = true;
		mainBGM.Stop ();
		gameoverBGM.Play ();
		objGameOverScore.text = score.ToString ();
		objScore.gameObject.SetActive (false);
		bool isHighScore = score > SaveController.GetHighScore ();
		if (isHighScore)
			SaveController.SetHighScore (score);
		objHighScore.SetActive (isHighScore);
		if (playCount >= 2) {
			playCount = 0;
			ShowAdsBtn ();
			return;
		}
		playCount++;
		ShowRetryBtn ();
	}

	void SetFilterSpeed () {
		filterSpeed += startfilterSpeed;
	}

	void Update () {
		if (isGameOver)
			return;

		if (filterOffsetY > 1.5f) {
			InitGameOver ();
			return;
		}

		if (score > 100 && speedMode == SpeedMode.Slow) {
			SetFilterSpeed ();
			speedMode = SpeedMode.Middle;
		}

		if (score > 300 && speedMode == SpeedMode.Middle) {
			SetFilterSpeed ();
			speedMode = SpeedMode.High;
		}

		if (score > 500 && speedMode == SpeedMode.High) {
			SetFilterSpeed ();
			speedMode = SpeedMode.Special;
		}

		if (down) {
			filterOffsetY -= startfilterSpeed * point;		
			timeCount++;
			if (TIME_COUNT_MAX == timeCount)
				down = false;
		} else {
			filterOffsetY += filterSpeed;
		}

		filter.transform.position = new Vector3 (filter.transform.position.x, filterOffsetY, filter.transform.position.z);
	

	}
}
