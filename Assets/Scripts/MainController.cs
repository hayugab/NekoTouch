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
	[SerializeField] AudioSource damageSound;
	[SerializeField] float filterSpeed;

	GameObject nekoPrefab;
	int score = 0;
	float filterOffsetY;
	bool down = false;
	int point = 0;

	// Use this for initialization
	void Start () {

		if (nekoPrefab == null)
			return;

		Init ();
	}

	public void Init () {
		filterOffsetY = -1f;
		point = 0;
		objGameOver.SetActive (false);

		foreach(Transform child in objField.gameObject.transform){
			Destroy(child.gameObject);
		}

		for (int i = 0; i < nekoNum; i++) {
			var nekoObj = Instantiate (nekoPrefab, objField.gameObject.transform);
			var neko = nekoObj.GetComponent<NekoController> ();
			neko.Init (this);
		}		
	}
		
	void Awake () {
		LoadResouces ();
	}

	void LoadResouces () {
		nekoPrefab = (GameObject)Resources.Load ("Prefabs/Neko");
	}

	public void AddNeko () {
		var nekoObj = Instantiate (nekoPrefab, objField.gameObject.transform);
		var neko = nekoObj.GetComponent<NekoController> ();
		neko.Init (this);
	}

	public void SetPoint (int point) {
		this.point = point;
	}

	void Update () {
		if (filterOffsetY > 1.5f) {
			objGameOver.SetActive (true);

//			Init ();
			return;
		}

		if (point == 0) {
			filterOffsetY += filterSpeed;
		} else {
			filterOffsetY += point * -0.1f;		
		}

		filter.transform.position = new Vector3 (filter.transform.position.x, filterOffsetY, filter.transform.position.z);
	
		point = 0;
	}
}
