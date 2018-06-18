using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectfilterController : MonoBehaviour {

	[SerializeField] GameObject rightObj;
	[SerializeField] GameObject leftObj;
	[SerializeField] Camera camera;

	// Use this for initialization
	void Start () {
		Vector3 topLeft = camera.ScreenToWorldPoint (Vector3.zero);
		Debug.Log (topLeft);
		rightObj.transform.position = new Vector3 (topLeft.x, rightObj.transform.position.y, rightObj.transform.position.z);
		leftObj.transform.position = new Vector3 (topLeft.x * -1.0f, leftObj.transform.position.y, leftObj.transform.position.z);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
