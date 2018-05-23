using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : MonoBehaviour {

	[SerializeField] AudioSource damageSound;
	[SerializeField] AudioSource pointSound;
	[SerializeField] BoxCollider2D home;
	[SerializeField] NekoController.NekoType type;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D c)
	{
		var neko = c.GetComponent<NekoController> ();
		Debug.Log (neko);


		if (type == neko.nekoType) {
			pointSound.Play ();
		} else if (type == NekoController.NekoType.Neko && neko.nekoType == NekoController.NekoType.NekoRainbow) {
			pointSound.Play ();
		} else {
			damageSound.Play ();
		}
	}

}
