﻿using UnityEngine;
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
	private List<GameObject> operators;
//	private int inputNodesAdded = 0;



	private OperatorFactory operatorFactory;
	void Start()
	{
		operatorFactory = new OperatorFactory();
		operators = new List<GameObject>();
		GetRandomEquation();
	}
	void Update()
	{
		if (Input.GetButtonDown("Randomize"))
		{
			GetRandomEquation();
		}
	}

	void ResetLevel()
	{
		ClearOperators();
		RemoveAllNodeLines();
		inputNodesAdded = 0;
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
		int solution = Random.Range(10, 100);
		AddSolution (solution);
		Operator op = operatorFactory.GetRandomOperator(level);
		AddOperator(op, nodePos1.position);
		EquationOperand equation = GetRandomEquation(solution, op);
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
		EquationOperand operand = op.GetRandomEquation(solution);
		return operand;
	}
	public void AddSolution(int solution)
	{
		goal.SetGoalValue(solution);

	}
	public void AddOperator(Operator op, Vector3 position)
	{
//		print (op.nodePath);
		GameObject pre = (GameObject)Resources.Load(op.nodePath, typeof(GameObject));
		GameObject go = Instantiate(pre) as GameObject; //So it's not pointing to a Prefab - It's pointing to a clone
		GameObject g = NGUITools.AddChild(this.GameNodeCanvas, go);
		Destroy(go);
		Vector3 pos = g.transform.position;
		pos.x += Random.Range(-.4f,.4f);
		pos.y += Random.Range(-.4f,.4f);
		g.transform.position = pos;
		operators.Add(g);
//		Instantiate(Resources.Load(op.nodePath), position, Quaternion.identity);
	}
	public void AddInputs(List<int> inputs)
	{
		for(int i = 0; i < inputs.Count; i++)
		{
			if(inputNodes.Count >= i + 1)
				inputNodes[i].SetInputValue(inputs[i]);
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
			//NGUITools.Destroy(op);
		}
		operators.Clear();
	}
}
