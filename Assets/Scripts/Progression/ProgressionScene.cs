using UnityEngine;
using System.Collections;

public class ProgressionScene : MonoBehaviour {
	public UIPopupList gameMode;
	public UILabel count, attempts;
	// Use this for initialization
	public void Start() {
//		UpdateToCurrentDifficulty();
	}

	public void SetGameMode(string mode) {
		if(mode == "Relax") {
			ProgressTracker.ActiveGameMode = GameMode.Relax;
		} else if(mode == "Time") {
			ProgressTracker.ActiveGameMode = GameMode.Timed;
		} else {
			ProgressTracker.ActiveGameMode = GameMode.Power;
		}
	}

	public void LoadGameScene () {
		StartCoroutine("AfterDelay");
	}


	IEnumerator AfterDelay() {
		yield return new WaitForSeconds(.4f);
		Application.LoadLevel("main");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
