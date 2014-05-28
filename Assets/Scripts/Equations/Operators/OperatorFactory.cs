using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OperatorFactory {
	private List<Operator> level1Operators;
	private List<Operator> level2Operators;
	// Use this for initialization

	public OperatorFactory()
	{
		level1Operators = new List<Operator>();
		level2Operators = new List<Operator>();
		level1Operators.Add(new AdditionOperator());
		level1Operators.Add (new SubtractionOperator());

	//	level2Operators.AddRange(level1Operators);
		level2Operators.Add(new MultiplicationOperator());
	}

	public List<Operator> GetOperators(Difficulty level)
	{
		if (level == Difficulty.Easy)
		{
			return level1Operators;
		}
		else
			return level2Operators;
	}
	public Operator GetRandomOperator(Difficulty level)
	{
		List<Operator> operators = GetOperators(level); 
		Operator randomOp = operators[Random.Range(0, operators.Count)]; //random Operator

		return randomOp;
	}
}
