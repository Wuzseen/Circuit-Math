using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	private static Game instance;
	public bool timedMode = false; 
	public GameObject countCube;
	public float initialTimerLength = 120; //seconds
	public float completedBonusTime = 3;
	public int correctAnswers;
	private float timeLeft;
	private float timeNoted;
	public UILabel timeLabel;
	public UILabel countLabel;
	public UILabel defeatLabel;
	public UILabel restartLabel;
	public static Game Instance {
		get { return instance; }
	}
	private List<GameNode> goalBuckets;
	// Use this for initialization
	void Awake () {
		goalBuckets = new List<GameNode>();
		instance = this;
		if (defeatLabel != null)
			defeatLabel.gameObject.SetActive(false);

		if (ProgressTracker.ActiveGameMode == GameMode.Timed) {
			timedMode = true;
			timeLeft = initialTimerLength;
			timeNoted = Time.time;
			StartCoroutine("TimeModeTimer");
		} else{
			if (timeLabel != null)
				timeLabel.gameObject.SetActive(false);
			if (countLabel != null)
				countLabel.gameObject.SetActive(false);
		}
	}

	void Start() {
		if(ProgressTracker.ActiveGameMode != GameMode.Timed) {
			restartLabel.text = "New Puzzle";
		}
	}

	IEnumerator TimeModeTimer() {
		while(true) {
			timeLeft -= Time.deltaTime;
			if(timeLeft <= 0) {
				timeLeft = 0;
				GameOver();
			}
			if(timeLeft != null) {
				timeLabel.text = Mathf.CeilToInt(timeLeft).ToString();
			}
			yield return 0;
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

	public void CorrectSolution()
	{
		timeLeft +=  completedBonusTime;
		correctAnswers++;
		Go.to (countCube.transform,.3f,new GoTweenConfig().rotation(new Vector3(0,90 * correctAnswers,0)));
		if (countLabel != null) {
			countLabel.text = correctAnswers.ToString();
		}
	}

	void GameOver()
	{
		Debug.Log ("Game Over");
		timedMode = false;
		if (defeatLabel != null)
		{
			defeatLabel.gameObject.SetActive(true);
		}
	}
}
