using UnityEngine;
using System.Collections;

public class ProgressionScene : MonoBehaviour {
	public UISlider progressSlider;
	public UILabel count, attempts;
	// Use this for initialization
	public void Start() {
		progressSlider.value = (float)ProgressTracker.Instance.SolvedCount / 200f;
		count.text = ProgressTracker.Instance.SolvedCount.ToString();
		attempts.text = string.Format("{0:D} Solve Attempts made.",ProgressTracker.Instance.Attempts);
	}

	public void LoadGameScene () {
		Application.LoadLevel("main");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
