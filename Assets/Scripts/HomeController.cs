using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : MonoBehaviour {

	[SerializeField] AudioSource damageSound;
	[SerializeField] AudioSource pointSound;
	[SerializeField] AudioSource rainbowSound;
 	[SerializeField] BoxCollider2D home;
	[SerializeField] NekoController.NekoType type;
	[SerializeField] MainController mainController;
	[SerializeField] Animation pointAnim;
	[SerializeField] Animation damageAnim;
	[SerializeField] Animation rainbowAnim;


	const int NORMAL_POINT = 1;
	const int DAMAGE_POINT = -3;
	const int SPECIAL_POINT = 5;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D c)
	{
		var neko = c.GetComponent<NekoController> ();

		switch (neko.nekoType) {
		case NekoController.NekoType.Inu:
		case NekoController.NekoType.Neko:
			if (type == neko.nekoType)
				NormalPoint (neko);
			else
				DamagePoint (neko);
			break;
		case NekoController.NekoType.NekoRainbow:
			if (type == NekoController.NekoType.Neko)
				SpecialPoint (neko);
			break;
		default:
			break;
		}
	}

	void NormalPoint(NekoController neko) {
		pointSound.Play ();
		pointAnim.Play ();
		mainController.SetPoint (NORMAL_POINT);
		mainController.DestroyNeko (neko);
		mainController.AddNeko ();
	}

	void DamagePoint (NekoController neko) {
		damageSound.Play ();
		damageAnim.Play ();
		mainController.SetPoint (DAMAGE_POINT);
		mainController.DestroyNeko (neko);
		mainController.AddNeko ();
	} 

	void SpecialPoint (NekoController neko) {
		if (rainbowSound != null) {
			rainbowSound.Play ();
			pointSound.Play ();
		}
		if (rainbowAnim != null) {
			rainbowAnim.Play ();
			pointAnim.Play ();
		}
		mainController.SetPoint (SPECIAL_POINT);
		mainController.DestroyNeko (neko);
		mainController.AddNeko ();
	}

}
