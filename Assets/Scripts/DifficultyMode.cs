using UnityEngine;
using System.Collections;

public enum Difficulty {
	Easy,
	Medium,
	Hard,
	Expert
}

public class DifficultyMode : MonoBehaviour {
	public static Difficulty SelectedDifficulty;
	public void UpdateDifficulty (string val) {
		if(val == "Easy") {
			SelectedDifficulty = Difficulty.Easy;
		} else if(val == "Medium") {
			SelectedDifficulty = Difficulty.Medium;
		} else if(val == "Hard") {
			SelectedDifficulty = Difficulty.Hard;
		} else {
			SelectedDifficulty = Difficulty.Expert;
		}
	}
}
