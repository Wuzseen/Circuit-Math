using UnityEngine;
using System.Collections;

public class ProgressionScene : MonoBehaviour {
	public UISlider progressSlider;
	public UIPopupList gameMode;
	public UILabel count, attempts;
	// Use this for initialization
	public void Start() {
		UpdateToCurrentDifficulty();
	}

	public void UpdateToCurrentDifficulty() {
		int cSolved = ProgressTracker.Instance.CurrentDifficultySolved();
		progressSlider.value = (float)cSolved / 15f;
		count.text = cSolved.ToString();
		attempts.text = string.Format("{0:D} Solve Attempts made.",ProgressTracker.Instance.CurrentDifficultyAttempts());
	}

	public void SetGameMode() {
		if(gameMode.value == "Relax") {
			ProgressTracker.ActiveGameMode = GameMode.Relax;
		} else if(gameMode.value == "Timed") {
			ProgressTracker.ActiveGameMode = GameMode.Timed;
		} else {
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
