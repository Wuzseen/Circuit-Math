using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OperatorFactory {
	private List<Operator> level1Operators;
	private List<Operator> level2Operators;
	private List<Operator> level3Operators;
	private List<Operator> level4Operators;
	// Use this for initialization

	public OperatorFactory()
	{
		level1Operators = new List<Operator>();
		level1Operators.Add(new AdditionOperator());
		level1Operators.Add(new SubtractionOperator());

		level2Operators = new List<Operator>();
		level2Operators.AddRange(level1Operators);
		level2Operators.Add(new MultiplicationOperator());
		
		level3Operators = new List<Operator>();
		level3Operators.AddRange(level2Operators);
		level3Operators.Add(new DivisionOperator());
		
		level4Operators = new List<Operator>();
		level4Operators.AddRange(level3Operators);
		level4Operators.Add(new SquareOperator());
	}

	public List<Operator> GetOperators(Difficulty level)
	{
		if (level == Difficulty.Easy)
		{
			return level1Operators;
		}
		else if (level == Difficulty.Medium)
		{
			return level2Operators;
		}
		else if (level == Difficulty.Hard)
		{
			return level3Operators;
		}
		else
		{
			return level4Operators;
		}
	}
	public Operator GetRandomOperator(Difficulty level)
	{
		List<Operator> operators = GetOperators(level); 
		Operator randomOp = operators[Random.Range(0, operators.Count)]; //random Operator

		return randomOp;
	}
}
