using UnityEngine;
using System.Collections;

public class ProgressionScene : MonoBehaviour {
	public UISlider progressSlider;
	public UILabel count;
	// Use this for initialization
	public void Start() {
		progressSlider.value = (float)ProgressTracker.Instance.SolvedCount / 200f;
		count.text = ProgressTracker.Instance.SolvedCount.ToString();
	}

	public void LoadGameScene () {
		Application.LoadLevel("main");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
