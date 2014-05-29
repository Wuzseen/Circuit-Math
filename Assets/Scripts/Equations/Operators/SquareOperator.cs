using UnityEngine;
using System.Collections;

public class SquareOperator : Operator {
	
	public SquareOperator()
	{
		symbol = '^';
		nodePath = "SquareNode";
		
	}	// Use this for initialization
	public override EquationOperand GetRandomEquation(int solution)
	{
		ValueOperand value1 = new ValueOperand((int)Mathf.Sqrt(solution));
		ValueOperand value2 = new ValueOperand(2);
		return new EquationOperand(value1, this, value2);
	}
	
	public override int Apply (Operand operand1, Operand operand2)
	{
		int solution = operand1.GetValue();
		for (int i = 1; i < operand2.GetValue(); i++) {
			solution *= operand1.GetValue();
		}
		return solution;
	}

	public static bool isPerfectSquare(int value)
	{
		return (Mathf.Sqrt(value) % 1 == 0);
	}

	public override int GetRandomValidSolution(int minNum, int maxNum)
	{
		int possibleSolution = Random.Range(minNum, maxNum + 1);
		while (!isPerfectSquare(possibleSolution))
		{
			possibleSolution = Random.Range(minNum, maxNum + 1);
		}

		Debug.Log ("Solution is " + possibleSolution);
		return possibleSolution;
	}
}
