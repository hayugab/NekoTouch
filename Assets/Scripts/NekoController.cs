﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NekoController : MonoBehaviour {
	MainController mainController;
	[SerializeField] GameObject[] nekos;
	[SerializeField] Rigidbody2D rigidbody;
	[SerializeField] BoxCollider2D box;
	[SerializeField] CircleCollider2D circle;
	bool isShow = false;
	bool isBingo = false;

	public enum NekoType {
		Neko = 0,
		Inu,
		NekoRainbow,
		InuRainbow
	}

	public NekoType nekoType;


	public void Init (MainController parent) {
		mainController = parent;
		ToggleNeko ();
	}

	public void Init () {
		ToggleNeko ();
	}

	void ToggleNeko () {
		int index = 0;
		index = UnityEngine.Random.Range (0, 80);
		if (index == 10)
			index = (int)NekoType.NekoRainbow;
		else if (index == 20)
			index = (int)NekoType.InuRainbow;
		else
			index = index % 2;
		SwitchNeko (index);
	}

	void ToggleNeko (NekoType type) {
		SwitchNeko ((int)type);
	}

	void SwitchNeko (int index) {
		nekoType = (NekoType)index;
		for (int i = 0; i < nekos.Length; i++) {
			if (i == index) {
				nekos[i].SetActive (true);
				continue;
			}
			nekos[i].SetActive (false);
		}		
	}

	void Start () {
	}

	void Awake () {
	}

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}

	private Vector3 screenPoint;
	private Vector3 offset;

	void OnMouseDown ()
	{

		if (mainController.IsGameOver || GameDataManager.gameDateManagerIsPause)
			return;
		iTween.PunchScale (this.gameObject, iTween.Hash (
			"x", 0.3f,
			"y", 0.3f,
			"time", 0.5
		));

//		 マウスカーソルは、スクリーン座標なので、
//		 対象のオブジェクトもスクリーン座標に変換してから計算する。

//		 このオブジェクトの位置(transform.position)をスクリーン座標に変換。
		screenPoint = Camera.main.WorldToScreenPoint (transform.position);
//		 ワールド座標上の、マウスカーソルと、対象の位置の差分。
		offset = transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		SwitchEnableCollider (false);
	}

	void OnMouseDrag ()
	{
		if (mainController.IsGameOver || GameDataManager.gameDateManagerIsPause)
			return;
		Vector3 currentScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint (currentScreenPoint) + this.offset;
		if (transform.position.y >= 1.2f) {
			OnMouseUp ();
			return;
		}
		transform.position = currentPosition;
	}

	void OnMouseUp () {
		if (mainController.IsGameOver || GameDataManager.gameDateManagerIsPause)
			return;
		iTween.PunchScale (this.gameObject, iTween.Hash (
			"x", 0.3f,
			"y", 0.3f,
			"time", 0.5
		));
		SwitchEnableCollider (true);
		rigidbody.AddForce (Vector3.up * 100);
	}

	void SwitchEnableCollider (bool enable) {
		box.enabled = enable;
		circle.enabled = enable;
	}
}
