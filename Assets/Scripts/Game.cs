using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	private static Game instance;
	public Color startColor, endColor;
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
	public UILabel finalScoreLabel;
	public UIProgressBar progress;
	public bool forceTimed; // for debugging
	public UITweener menuTween;
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

		if (ProgressTracker.ActiveGameMode == GameMode.Timed || forceTimed) {
			timedMode = true;
			timeLeft = initialTimerLength;
			timeNoted = Time.time;
			StartCoroutine("TimeModeTimer");
		} else {
			timeLeft = 0f;
			if (timeLabel != null) {
				timeLabel.gameObject.SetActive(false);
			}
			if (progress != null) {
				progress.gameObject.SetActive(false);
			}
		}
	}

	void Start() {
		if(ProgressTracker.ActiveGameMode != GameMode.Timed) {
			restartLabel.text = "New Puzzle";
		}
	}

	IEnumerator TimeModeTimer() {
		float startTime = timeLeft;
		while(true) {
			timeLeft -= Time.deltaTime;
			if(timeLeft <= 0) {
				timeLeft = 0;
				timeLabel.text = "0";
				GameOver();
				break;
			}
			if(timeLabel != null) {
				progress.value = timeLeft/startTime;
				progress.foregroundWidget.color = Color.Lerp(startColor,endColor,progress.value);
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
		print("Correct");
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
		GameObject[] ops = GameObject.FindGameObjectsWithTag("OperatorNode");
		GameObject[] other = GameObject.FindGameObjectsWithTag("GameNode");
		foreach(GameObject o in ops) {
			o.SetActive(false);
		}

		foreach(GameObject o in other) {
			o.SetActive(false);
		}
		StartCoroutine("DefeatRoutine");
	}

	IEnumerator DefeatRoutine() {
		menuTween.PlayForward();
		yield return new WaitForSeconds(2.5f);
		Go.to (finalScoreLabel, .8f,new GoTweenConfig().floatProp("alpha",1f));
		Go.to (defeatLabel,.3f,new GoTweenConfig().floatProp("alpha",0));
		countLabel.ResetAnchors();
		countLabel.transform.positionTo(2f,Vector3.zero);
		yield return 1.5f;
		int c = correctAnswers;
		while(true) {
			Go.to (countCube.transform,.3f,new GoTweenConfig().rotation(new Vector3(0,90 * (c % 4),0)));
			yield return new WaitForSeconds(Random.Range(1f,3f));
			c++;
		}
	}
}
