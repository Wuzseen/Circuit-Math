using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomizerArgs {
	public Randomizer _Randomizer;
	public RandomizerArgs(Randomizer _randomizer) {
		_Randomizer = _randomizer;
	}
}

public class Randomizer : MonoBehaviour {
	public delegate void RandomizerEventHandler(RandomizerArgs args);
	public static event RandomizerEventHandler OnPuzzleCreated;
	public GameObject GameNodeCanvas;
	public Difficulty level = Difficulty.Easy;
	public int maxNum = 50;
	public int maxNumOperators = 1;
	public GameInputNode inputNode;
	public GoalNode goal;
	public Transform nodePos1;
	public Transform nodePos2;
	public UIPopupList difficultyPopup;
	public List<GameInputNode> inputNodes;
//	private int inputNodesAdded = 0;
	private List<GameObject> operators;
	private bool ignoreFirstDiffSwap = true;
	private OperatorFactory operatorFactory;
	void Start()
	{
		operatorFactory = new OperatorFactory();
		operators = new List<GameObject>();
		level = DifficultyMode.SelectedDifficulty;
		GetRandomEquation();
	}
	void Update()
	{
		if (Input.GetButtonDown("Randomize"))
		{
			GetRandomEquation();
		}
	}

	public void DifficultySwap() {
		if(ignoreFirstDiffSwap) {
			ignoreFirstDiffSwap = false;
			return;
		}
		if(difficultyPopup.value == "Easy") {
			DifficultyMode.SelectedDifficulty = Difficulty.Easy;
		} else if(difficultyPopup.value == "Medium") {
			DifficultyMode.SelectedDifficulty = Difficulty.Medium;
		} else if(difficultyPopup.value == "Hard") {
			DifficultyMode.SelectedDifficulty = Difficulty.Hard;
		} else {
			DifficultyMode.SelectedDifficulty = Difficulty.Expert;
		}
		Application.LoadLevel("main");
		ignoreFirstDiffSwap = true;
	}

	void ResetLevel()
	{
		ClearOperators();
		RemoveAllNodeLines();
//		inputNodesAdded = 0;
	}

	void RemoveAllNodeLines()
	{
		foreach(NodeLine line in GameObject.FindObjectsOfType<NodeLine>())
		{
			Destroy(line.gameObject);
		}
	}

	public void GetRandomEquation()
	{
		ResetLevel();
		//Only using one operator right now
		Operator op = operatorFactory.GetRandomOperator(level);
		int solution = op.GetRandomValidSolution(10, maxNum);
		AddSolution (solution);
		List<Operator> ops = new List<Operator>();

//		AddOperator(op, nodePos1.position);
		ops.Add(op);
		EquationOperand equation = GetRandomEquation(solution, op);

		int numOperators = Random.Range(1, maxNumOperators + 1);
		for(int i = 1; i < numOperators; i++){
			int operandNumber = Random.Range(0, 2);
			EquationOperand eo; 
			if (operandNumber == 0)
			{
				eo = GetRandomEquation(equation.operand1.GetValue(), operatorFactory.GetRandomOperator(level));
				equation.operand1 = eo;
				ops.Add(eo._operator);
//				AddOperator(eo._operator, nodePos2.position);
			}
			else if (op.GetType() != typeof(SquareOperator)) // make sure not to change the 2 in x ^ 2
			{
				eo = GetRandomEquation(equation.operand2.GetValue(), operatorFactory.GetRandomOperator(level));
				equation.operand2 = eo;
				ops.Add(eo._operator);
//				AddOperator(eo._operator, nodePos2.position);
			}
		}
		AddOps(ops);
		List<int> inputValues = GetInputValues(equation, new List<int>());
		AddInputs(inputValues);
		Debug.Log(equation.ToString() + " = " + equation.GetValue());
		if(OnPuzzleCreated != null) {
			OnPuzzleCreated(new RandomizerArgs(this));
		}
	}
	public EquationOperand GetRandomEquation(int solution, Operator op)
	{
		EquationOperand operand = op.GetRandomEquation(solution);
		return operand;
	}
	public void AddSolution(int solution)
	{
		goal.SetGoalValue(solution);

	}

	public void AddOps(List<Operator> ops) {
		float yOffset = .3f;
		float xOffset = .5f;
		if(ops.Count == 1) {
			AddOperator(ops[0],Vector3.zero);
			return;
		}

		if(ops.Count == 2) {
			AddOperator(ops[0],new Vector3(0,-yOffset,0));
			AddOperator(ops[1],new Vector3(0,yOffset,0));
			return;
		}

		if(ops.Count == 3) {
			AddOperator(ops[0],new Vector3(-xOffset,yOffset,0));
			AddOperator(ops[1],new Vector3(xOffset,0,0));
			AddOperator(ops[2],new Vector3(-xOffset,-yOffset,0));
			return;
		}
		
		if(ops.Count == 4) {
			AddOperator(ops[0],new Vector3(-xOffset,yOffset,0));
			AddOperator(ops[1],new Vector3(xOffset,yOffset,0));
			AddOperator(ops[2],new Vector3(-xOffset,-yOffset,0));
			AddOperator(ops[3],new Vector3(xOffset,-yOffset,0));
			return;
		}
	}

	public void AddOperator(Operator op, Vector3 position)
	{
//		print (op.nodePath);
		GameObject pre = (GameObject)Resources.Load(op.nodePath, typeof(GameObject));
		GameObject go = Instantiate(pre) as GameObject; //So it's not pointing to a Prefab - It's pointing to a clone
		GameObject g = NGUITools.AddChild(this.GameNodeCanvas, go);
		Destroy(go);
		g.transform.position = position;
		operators.Add(g);
//		Instantiate(Resources.Load(op.nodePath), position, Quaternion.identity);
	}
	public void AddInputs(List<int> inputs)
	{
		// add random numbers until inputs.Count == inputNodes.Count
		for (int i = inputs.Count; i < inputNodes.Count; i++)
		{
			inputs.Add(Random.Range(1, 100));
		}

		// randomly place input values into an array
		int[] ins = new int[inputNodes.Count];
		for (int i = 0; i < ins.Length; i++)
		{
			int which = Random.Range(0, inputs.Count); // get random remaining input
			ins[i] = inputs[which]; // put input value into array
			inputs.RemoveAt(which); // remove input from the list
		}

		for(int i = 0; i < ins.Length; i++)
		{
			if(inputNodes.Count >= i + 1)
				inputNodes[i].SetInputValue(ins[i]);
			else
			{
				Debug.Log ("Wrong number of inputs?");
				//Add a new node or something... idk
			}
		}
	}
	public List<int> GetInputValues(EquationOperand equation, List<int> inputVals)
	{
		if (equation.operand1.GetType() == typeof(ValueOperand))
		{
			inputVals.Add(equation.operand1.GetValue());
		}
		else
		{
			inputVals = GetInputValues((EquationOperand) equation.operand1, inputVals);
		}
		
		if (equation.operand2.GetType() == typeof(ValueOperand))
		{
			inputVals.Add (equation.operand2.GetValue());
		}
		else
		{
			inputVals = GetInputValues((EquationOperand) equation.operand2, inputVals);

		}
		return inputVals;
	}

	void ClearOperators()
	{
		foreach(GameObject op in operators)
		{
			op.SetActive(false);
//			NGUITools.Destroy(op);
		}
		operators.Clear();
	}
}
