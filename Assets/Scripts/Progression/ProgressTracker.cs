using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DifficultyProgress {
	public int Solved;
	public int Attempts;
	public int Seen;
	public DifficultyProgress(int _count, int _attempts, int _seen) {
		Solved = _count;
		Attempts = _attempts;
		Seen = _seen;
	}
}

public enum GameMode {
	Career,
	Relax,
	Timed
}

public class ProgressTracker : MonoBehaviour {
	public static GameMode ActiveGameMode;
	public static string EasyString = "Easy";
	public static string MediumString = "Normal";
	public static string HardString = "Hard";
	public static string ExpertString = "Expert";
	public static string TotalSolvedString = "TotalSolved";
	public static string TotalAttemptsString = "TotalAttempts"; // Times check button was hit
	public static string TotalPuzzlesString = "TotalPuzzles";
	public static ProgressTracker Instance; // Oh the singleton pattern, laziness incarnate
	public Dictionary<string,DifficultyProgress> ProgressionDict;
	// Use this for initialization
	void Awake () {
		if(Instance != null) {
			Destroy (this.gameObject);
			return;
		}
		Instance = this;
		SolverCheck.OnSolveAttempt += SolveAttempt;
		Randomizer.OnPuzzleCreated += NewPuzzle;
		string[] diffStrings = new string[]{EasyString,MediumString,HardString,ExpertString};
		ProgressionDict = new Dictionary<string, DifficultyProgress>();
		foreach(string s in diffStrings) {
			int solved = PrefCount(PrefKey(s,TotalSolvedString)); 
			int attempted = PrefCount(PrefKey(s,TotalAttemptsString));
			int seen = PrefCount(PrefKey(s,TotalPuzzlesString));
			ProgressionDict.Add(s,new DifficultyProgress(solved,attempted,seen));
		}
	}

	string PrefKey(string difficultyString,string itemString) {
		return string.Format("{0}_{1}",difficultyString,itemString);
	}

	string DifficultyString() {
		string currDiffString = "";
		if(DifficultyMode.SelectedDifficulty == Difficulty.Easy) {
			currDiffString = EasyString;
		} else if(DifficultyMode.SelectedDifficulty == Difficulty.Medium) {
			currDiffString = MediumString;
		} else if(DifficultyMode.SelectedDifficulty == Difficulty.Hard) {
			currDiffString = HardString;
		} else {
			currDiffString = ExpertString;
		}
		return currDiffString;
	}

	string PrefKeyFromDifficulty(string itemString) {
		return PrefKey(DifficultyString(),itemString);
	}

	int PrefCount(string pref) {
		if(PlayerPrefs.HasKey(pref)) {
			return PlayerPrefs.GetInt(pref);
		}
		return 0;
	}
	int DifficultySolveCount(string difficulty) {
		return ProgressionDict[difficulty].Solved;
	}

	public int EasyPuzzlesSolved() {
		return DifficultySolveCount(EasyString);
	}

	public int MediumPuzzlesSolved() {
		return DifficultySolveCount(MediumString);
	}

	public int HardPuzzlesSolved() {
		return DifficultySolveCount(HardString);
	}

	public int ExpertPuzzlesSolved() {
		return DifficultySolveCount(ExpertString);
	}

	public int CurrentDifficultySolved() {
		return DifficultySolveCount(DifficultyString());
	}
	
	public int CurrentDifficultyAttempts() {
		return ProgressionDict[DifficultyString()].Attempts;
	}

	void NewPuzzle(RandomizerArgs args) {
		string diff = DifficultyString();
		ProgressionDict[diff].Seen++;
		PlayerPrefs.SetInt(PrefKey(diff,TotalPuzzlesString),ProgressionDict[diff].Seen);
	}

	private void UpdateAttempts(int toAdd) {
		string diff = DifficultyString();
		ProgressionDict[diff].Attempts += toAdd;
		PlayerPrefs.SetInt(PrefKey(diff,TotalAttemptsString),ProgressionDict[diff].Attempts);
	}

	private void UpdateTotalSolved(int toAdd) {
		string diff = DifficultyString();
		ProgressionDict[diff].Solved += toAdd;
		PlayerPrefs.SetInt(PrefKey(diff,TotalSolvedString),ProgressionDict[diff].Solved);
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
