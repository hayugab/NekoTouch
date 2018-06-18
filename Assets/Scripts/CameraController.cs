using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//画面のアスペクト比を取得
		float deviceAspect = (float)Screen.width / (float)Screen.height;
		var camera = this.GetComponent<Camera> ();
//		var cameraSize = camera.orthographicSize;
		Debug.Log(deviceAspect);
		if (deviceAspect < 0.5f) {
			camera.orthographicSize = 2.4f;
			camera.transform.position = new Vector3 (camera.transform.position.x, camera.transform.position.y - 0.4f, camera.transform.position.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
