using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	private static Game instance;
	public static Game Instance {
		get { return instance; }
	}
	private List<GameNode> goalBuckets;
	// Use this for initialization
	void Awake () {
		goalBuckets = new List<GameNode>();
		instance = this;
	}

	public void AddGoalBucket(GameNode n) {
		goalBuckets.Add(n);
	}

	public bool IsSolved() {
		foreach(GameNode n in goalBuckets) {
			if(n.NodeValue == 0)
				return false;
		}
		return true;
	}
}
