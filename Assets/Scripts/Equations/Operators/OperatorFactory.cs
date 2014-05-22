using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OperatorFactory {
	private List<Operator> level1Operators;
	// Use this for initialization

	public OperatorFactory()
	{
		level1Operators = new List<Operator>();
		level1Operators.Add(new AdditionOperator());
		level1Operators.Add (new SubtractionOperator());
	}

	public List<Operator> GetOperators(int level)
	{
		//if (level == 1)
	//	{
			return level1Operators;
	//	}
	}
	public Operator GetRandomOperator(int level)
	{
		List<Operator> operators = GetOperators(level); 
		return operators[Random.Range(0, operators.Count)]; //random Operator
	}
}
