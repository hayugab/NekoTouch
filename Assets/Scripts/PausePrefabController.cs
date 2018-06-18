using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePrefabController : MonoBehaviour {

	public void SetPlay() {
		Time.timeScale = 1f;
		GameDataManager.gameDateManagerIsPause = false;
		Destroy (this.gameObject);
	}
}
