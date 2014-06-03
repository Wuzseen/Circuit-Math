using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CallOnPress : MonoBehaviour {
	public List<EventDelegate> toCall;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPress(bool isDown) {
		if(isDown) {
			if(toCall != null)
				EventDelegate.Execute(toCall);
		}
	}
}
