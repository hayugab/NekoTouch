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
	[SerializeField] float filterSpeed;
	[SerializeField] AudioSource btnSound;

	List<NekoController> nekoList = new List<NekoController> ();

	GameObject nekoPrefab;
	int score = 0;
	float filterOffsetY;
	bool down = false;
	bool isGameOver = false;
	int point = 0;
	int timeCount = 0;


	const int TIME_COUNT_MAX = 10;


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
		isGameOver = false;
		textScore.text = score.ToString ();
		objGameOver.SetActive (false);

		foreach(Transform child in objField.gameObject.transform){
			Destroy(child.gameObject);
		}

		for (int i = 0; i < nekoNum; i++) {
			var nekoObj = Instantiate (nekoPrefab, objField.gameObject.transform);
			var neko = nekoObj.GetComponent<NekoController> ();
			neko.Init (this);
			nekoList.Add (neko);
		}		
	}

	public void retryInit() {
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

	public void DestroyAllNeko() {
		foreach (var neko in nekoList) {
			nekoList.Remove (neko);
			Destroy (neko.gameObject);
		}
		Init ();
	}

	public void AddNeko () {
		var nekoObj = Instantiate (nekoPrefab, objField.gameObject.transform);
		var neko = nekoObj.GetComponent<NekoController> ();
		neko.Init (this);
		nekoList.Add (neko);
	}

	public void SetPoint (int point) {
		this.point = point;
		score += point * 5;
		textScore.text = score.ToString ();
		down = true;
		timeCount = 0;
	}

	void Update () {
		if (filterOffsetY > 1.5f) {
			objGameOver.SetActive (true);
			isGameOver = true;
			return;
		}

		if (down) {
			filterOffsetY -= filterSpeed * point;		
			timeCount++;
			if (TIME_COUNT_MAX == timeCount)
				down = false;
		} else {
			filterOffsetY += filterSpeed;
		}

		filter.transform.position = new Vector3 (filter.transform.position.x, filterOffsetY, filter.transform.position.z);
	

	}
}
