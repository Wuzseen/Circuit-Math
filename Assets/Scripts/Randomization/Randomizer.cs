using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Randomizer : MonoBehaviour {
	public GameObject GameNodeCanvas;
	public int level = 1;
	public int maxNum = 100;
	public int maxNumOperators = 1;
	public GameInputNode inputNode;
	public GoalNode goal;
	public Transform nodePos1;
	public Transform nodePos2;
	public List<GameInputNode> inputNodes;
	private int inputNodesAdded = 0;



	private OperatorFactory operatorFactory;
	void Start()
	{
		operatorFactory = new OperatorFactory();

		GetRandomEquation();
	}
	void Update()
	{
		if (Input.GetButtonDown("Randomize"))
		{
			GetRandomEquation();
		}
	}

	public void GetRandomEquation()
	{
		//Only using one operator right now
		int solution = Random.Range(10, 100);
		AddSolution (solution);
		Operator op = operatorFactory.GetRandomOperator(level);
		AddOperator(op, nodePos1.position);
		EquationOperand equation = GetRandomEquation(solution, op);
		Debug.Log("Initial " + equation.ToString());
		int numOperators = Random.Range(1, maxNumOperators + 1);
		for(int i = 1; i < numOperators; i++){
			int operandNumber = Random.Range(0, 2);
			EquationOperand eo; 

			if (operandNumber == 0)
			{
				eo = GetRandomEquation(equation.operand1.GetValue(), operatorFactory.GetRandomOperator(level));
				equation.operand1 = eo;

			}
			else 
			{
				eo = GetRandomEquation(equation.operand2.GetValue(), operatorFactory.GetRandomOperator(level));
				equation.operand2 = eo;

			}
			AddOperator(eo._operator, nodePos2.position);
		}
		List<int> inputValues = GetInputValues(equation, new List<int>());
		AddInputs(inputValues);
		Debug.Log(equation.ToString() + " = " + equation.GetValue());

	}
	public EquationOperand GetRandomEquation(int solution, Operator op)
	{
		Debug.Log ("Operator" + op.symbol);
		EquationOperand operand = op.GetRandomEquation(solution);
		return operand;
	}
	public void AddSolution(int solution)
	{
		goal.GoalValue = solution;
	}
	public void AddOperator(Operator op, Vector3 position)
	{
//		print (op.nodePath);
		GameObject pre = (GameObject)Resources.Load(op.nodePath);
		NGUITools.AddChild(this.GameNodeCanvas,pre);
//		Instantiate(Resources.Load(op.nodePath), position, Quaternion.identity);
	}
	public void AddInputs(List<int> inputs)
	{
		Debug.Log ("Coubt = " + inputs.Count);
		for(int i = 0; i < inputs.Count; i++)
		{
			if(inputNodes.Count >= i + 1)
				inputNodes[i].inputValue = inputs[i];
			else
			{
				Debug.Log ("Here");
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
		Debug.Log("Val[0] = " + inputVals[0]);

		
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
}
