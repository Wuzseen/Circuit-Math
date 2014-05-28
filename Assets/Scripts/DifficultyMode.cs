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
	public UIPopupList popup;

	public void UpdateDifficulty () {
		if(popup.value == "Easy") {
			SelectedDifficulty = Difficulty.Easy;
		} else if(popup.value == "Medium") {
			SelectedDifficulty = Difficulty.Medium;
		} else if(popup.value == "Hard") {
			SelectedDifficulty = Difficulty.Hard;
		} else {
			SelectedDifficulty = Difficulty.Expert;
		}
	}
}
