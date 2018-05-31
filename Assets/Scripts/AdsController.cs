using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour
{
	[SerializeField] MainController mainController;

	public void ShowRewardedAd()
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			Advertisement.Show("rewardedVideo");
		}
		StartCoroutine (WaitFotButtonHide());
	}

	IEnumerator WaitFotButtonHide () {
		yield return new WaitForSeconds(2.0f);
		mainController.ShowRetryBtn ();
	}
//
//	private void HandleShowResult(ShowResult result)
//	{
//		switch (result)
//		{
//		case ShowResult.Finished:
//			Debug.Log("The ad was successfully shown.");
//			//
//			// YOUR CODE TO REWARD THE GAMER
//			// Give coins etc.
//			break;
//		case ShowResult.Skipped:
//			Debug.Log("The ad was skipped before reaching the end.");
//			break;
//		case ShowResult.Failed:
//			Debug.LogError("The ad failed to be shown.");
//			break;
//		}
//	}
}