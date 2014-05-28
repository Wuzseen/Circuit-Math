using UnityEngine;
using System.Collections;

public class SolverArgs {
	public bool IsSolved;
	public SolverArgs(bool isSolved) {
		IsSolved = isSolved;
	}
}

public class SolverCheck : MonoBehaviour {
	public delegate void SolverEventHandler(SolverArgs args);
	public static event SolverEventHandler OnSolveAttempt;
	private Randomizer randomizer;
	private Game game;
	// Use this for initialization
	void Start () {
		randomizer = GameObject.FindObjectOfType<Randomizer>() as Randomizer;
		game = GameObject.FindObjectOfType<Game>() as Game;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CheckSolve() {
		bool solved = Game.Instance.IsSolved();
		if(solved) {
			print ("SOLVED");
			randomizer.GetRandomEquation();
			if (ProgressTracker.ActiveGameMode == GameMode.Timed)
			{
				game.CorrectSolution();
			}
		} else {
			print ("WRONG");
		}
		if(OnSolveAttempt != null) {
			OnSolveAttempt(new SolverArgs(solved));
		}
	}

	void OnPress (bool isDown) {
		if(isDown == false) {
			CheckSolve();
		}
	}
}
