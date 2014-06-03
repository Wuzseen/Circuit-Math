using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {
	public UISprite diffSelect;
	public ProgressionScene psObject;
	public UISprite BeginGame;
	public UISprite Easy, Medium, Hard, Expert;
	public UISprite Career, Relax, Timed;
	private int difficultiesOn, modesOn;
	private NodeLine difficultyLine, modeLine;
	// Use this for initialization
	void Start () {
		BeginGame.alpha = 0;
		difficultiesOn = 1;
		Easy.alpha = 0;
		Medium.alpha = 0;
		Hard.alpha = 0;
		Expert.alpha = 0;
		Career.alpha = 0;
		Relax.alpha = 0;
		Timed.alpha = 0;
		diffSelect.alpha = 0;
//		ToggleDifficulties();
//		modesOn = 0;
//		ToggleModes();
		modesOn = 1;
	}

	public void ToggleDifficulties() {
		Go.to (Easy,.4f, new GoTweenConfig().floatProp("alpha", 1f * difficultiesOn));
		Go.to (Medium,.4f, new GoTweenConfig().floatProp("alpha", 1f * difficultiesOn));
		Go.to (Hard,.4f, new GoTweenConfig().floatProp("alpha", 1f * difficultiesOn));
		Go.to (Expert,.4f, new GoTweenConfig().floatProp("alpha", 1f * difficultiesOn));
//		difficultiesOn = Mathf.Abs(difficultiesOn - 1);
	}
	
	public void ToggleModes() {
		Go.to (Career,.4f, new GoTweenConfig().floatProp("alpha", 1f * modesOn));
		Go.to (Relax,.4f, new GoTweenConfig().floatProp("alpha", 1f * modesOn));
		Go.to (Timed,.4f, new GoTweenConfig().floatProp("alpha", 1f * modesOn));
//		modesOn = Mathf.Abs(modesOn - 1);
	}

	public void MakeDifficultyLine(Transform a, Transform b) {
		if(difficultyLine != null) {
			difficultyLine.B = b;
		} else {
			difficultyLine = NodeLine.CreateNewLine(a,b,Color.green,Color.green);
		}
		if(modeLine != null) {
			MakeModeLine(modeLine.B);
		}
		if(ProgressTracker.ActiveGameMode == GameMode.Career)
			psObject.UpdateToCurrentDifficulty();
	}
	
	public void MakeModeLine(Transform t) {
		if(modeLine != null) {
			modeLine.A = difficultyLine.B;
			modeLine.B = t;
		} else {
			modeLine = NodeLine.CreateNewLine(difficultyLine.B,t,Color.green,Color.green);
			Go.to (BeginGame,.4f, new GoTweenConfig().floatProp("alpha", 1f));
		}
	}

	public void SetDifficulty() {
		ToggleModes();
	}

	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown) {
			Go.to (diffSelect,.4f, new GoTweenConfig().floatProp("alpha", 1f));
		}
	}
}
