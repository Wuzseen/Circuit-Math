using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	public delegate void PauseHandler(bool paused);
	public static PauseHandler OnPause;
	public UISprite pauseSprite;
	private bool paused = false;

	void OnPress(bool isDown) {
		if(isDown == true) {
			paused = !paused;
			if(paused) {
				pauseSprite.name = "menuDown";
			} else {
				pauseSprite.name = "menuUp";
			}
			if(OnPause != null) {
				OnPause(paused);
			}
		}
	}
}
