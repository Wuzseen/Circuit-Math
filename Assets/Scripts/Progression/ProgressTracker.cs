using UnityEngine;
using System.Collections;

public class ProgressTracker : MonoBehaviour {
	private int _solvedCount;
	public int SolvedCount {
		get {
			return _solvedCount;
		}
	}

	private int _attempts;
	public int Attempts {
		get {
			return _attempts;
		}
	}

	private int _puzzlesSeen;
	public int PuzzlesSeen {
		get {
			return _puzzlesSeen;
		}
	}

	public static string TotalSolvedString = "TotalSolved";
	public static string TotalAttemptsString = "TotalAttempts"; // Times check button was hit
	public static ProgressTracker Instance; // Oh the singleton pattern, laziness incarnate
	// Use this for initialization
	void Awake () {
		if(Instance != null) {
			Destroy (this.gameObject);
			return;
		}
		Instance = this;
		SolverCheck.OnSolveAttempt += SolveAttempt;
		if(PlayerPrefs.HasKey(TotalSolvedString)) {
			_solvedCount = PlayerPrefs.GetInt(TotalSolvedString);
		} else {
			_solvedCount = 0;
		}
		if(PlayerPrefs.HasKey(TotalAttemptsString)) {
			_attempts = PlayerPrefs.GetInt(TotalAttemptsString);
		} else {
			_attempts = 0;
		}
	}

	private void UpdateAttempts(int toAdd) {
		_attempts += toAdd;
		PlayerPrefs.SetInt(TotalAttemptsString,_attempts);
	}

	private void UpdateTotalSolved(int toAdd) {
		_solvedCount += toAdd;
		PlayerPrefs.SetInt(TotalSolvedString,_solvedCount);
		UpdateAttempts(toAdd);
	}

	void SolveAttempt(SolverArgs args) {
		if(args.IsSolved) {
			UpdateTotalSolved(1);
		} else {
			UpdateAttempts(1);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
