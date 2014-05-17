using UnityEngine;
using System.Collections;

public class SolverButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPress (bool isDown) {
		if(isDown == false) {
			if(Game.Instance.IsSolved())
				print ("SOLVED");
			else
				print ("WRONG");
		}
	}
}
