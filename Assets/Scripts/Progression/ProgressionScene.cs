using UnityEngine;
using System.Collections;

public class ProgressionScene : MonoBehaviour {
	public UISlider progressSlider;
	public UIPopupList gameMode;
	public UILabel count, attempts;
	// Use this for initialization
	public void Start() {
//		UpdateToCurrentDifficulty();
	}

	public void UpdateToCurrentDifficulty() {
		if(ProgressTracker.ActiveGameMode != GameMode.Career) {
			return;
		}
		int cSolved = ProgressTracker.Instance.CurrentDifficultySolved();
		progressSlider.value = (float)cSolved / 15f;
		count.text = cSolved.ToString();
//		attempts.text = string.Format("{0:D} Solve Attempts made.",ProgressTracker.Instance.CurrentDifficultyAttempts());
	}

	public void SetGameMode(string mode) {
		if(mode == "Relax") {
			Go.to (progressSlider,.4f, new GoTweenConfig().floatProp("alpha", 0f));
			ProgressTracker.ActiveGameMode = GameMode.Relax;
		} else if(mode == "Time") {
			Go.to (progressSlider,.4f, new GoTweenConfig().floatProp("alpha", 0f));
			ProgressTracker.ActiveGameMode = GameMode.Timed;
		} else {
			progressSlider.gameObject.SetActive(true);
			progressSlider.alpha = 0;
			Go.to (progressSlider,.4f, new GoTweenConfig().floatProp("alpha", 1f));
			ProgressTracker.ActiveGameMode = GameMode.Career;
		}
	}

	public void LoadGameScene () {
		Application.LoadLevel("main");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
