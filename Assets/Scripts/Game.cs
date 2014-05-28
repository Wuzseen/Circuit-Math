using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	private static Game instance;
	public bool timedMode = false; 
	public float initialTimerLength = 120; //seconds
	public float completedBonusTime = 3;
	public int correctAnswers;
	private float timeLeft;
	private float timeNoted;
	public static Game Instance {
		get { return instance; }
	}
	private List<GameNode> goalBuckets;
	// Use this for initialization
	void Awake () {
		goalBuckets = new List<GameNode>();
		instance = this;
		if (ProgressTracker.ActiveGameMode == GameMode.Timed)
		{
			timedMode = true;
			timeLeft = initialTimerLength;
			timeNoted = Time.time;
		}
		else{
			Debug.Log (ProgressTracker.ActiveGameMode);
		}
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

	void Update()
	{
		if (timedMode)
		{
			timeLeft -= (Time.time - timeNoted);
			timeNoted = Time.time;
			Debug.Log (Mathf.FloorToInt(timeLeft).ToString());
			if (timeLeft <= 0)
			{
				GameOver();
			}
		}
	}
	public void CorrectSolution()
	{
		timeLeft +=  completedBonusTime;
		correctAnswers++;
	}

	void GameOver()
	{
		Debug.Log ("Game Over");
		timedMode = false;
	}
}
