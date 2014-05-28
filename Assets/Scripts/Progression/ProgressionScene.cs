using UnityEngine;
using System.Collections;

public class ProgressionScene : MonoBehaviour {
	public UISlider progressSlider;
	public UILabel count, attempts;
	// Use this for initialization
	public void Start() {
		UpdateToCurrentDifficulty();
	}

	public void UpdateToCurrentDifficulty() {
		int cSolved = ProgressTracker.Instance.CurrentDifficultySolved();
		progressSlider.value = (float)cSolved / 200f;
		count.text = cSolved.ToString();
		attempts.text = string.Format("{0:D} Solve Attempts made.",ProgressTracker.Instance.CurrentDifficultyAttempts());
	}

	public void LoadGameScene () {
		Application.LoadLevel("main");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
