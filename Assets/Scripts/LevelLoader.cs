using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	public string levelName;

	void OnPress () {
        Application.LoadLevel(levelName);	
	}
}
