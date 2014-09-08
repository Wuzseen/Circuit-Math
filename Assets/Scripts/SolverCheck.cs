using UnityEngine;
using System.Collections;

public class SolverArgs {
	public bool IsSolved;
	public SolverArgs(bool isSolved) {
		IsSolved = isSolved;
	}
}

public class SolverCheck : MonoBehaviour {
	public AudioClip successClipSound;
	public static SolverCheck Instance;
	public delegate void SolverEventHandler(SolverArgs args);
	public static event SolverEventHandler OnSolveAttempt;
	public UILabel winText;
	private Randomizer randomizer;
	private Game game;
	// Use this for initialization
	void Start () {
		Instance = this;
		randomizer = GameObject.FindObjectOfType<Randomizer>() as Randomizer;
		game = GameObject.FindObjectOfType<Game>() as Game;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator newPuzzleRoutine() {
		SoundManager.PlaySFX(successClipSound);
		Go.to (winText,.2f,new GoTweenConfig().floatProp("alpha",255f));
		yield return new WaitForSeconds(2f);
		Fractal.FractalMaster.TurnAllOff();
		randomizer.GetRandomEquation();
		Go.to (winText,.2f,new GoTweenConfig().floatProp("alpha",0f));
	}

	public void CheckSolve() {
		bool solved = game.IsSolved();
		if(solved) {
			print ("SOLVED");
			Fractal.FractalMaster.TurnAllOn();
			StartCoroutine("newPuzzleRoutine");
			//			randomizer.GetRandomEquation();
			game.CorrectSolution();
		} else {
			Fractal.FractalMaster.RandomBGChange();
		}
		if(OnSolveAttempt != null) {
			OnSolveAttempt(new SolverArgs(solved));
		}
	}
}
