using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour {

	public static void ShowAd() {
		if (Advertisement.IsReady ()) {
			Advertisement.Show ();
		}
	}
}
