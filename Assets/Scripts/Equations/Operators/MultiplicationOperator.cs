using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplicationOperator : Operator {

	public MultiplicationOperator()
	{
		symbol = '*';
		nodePath = "MultiplyNode";
		
	}	// Use this for initialization
	public override EquationOperand GetRandomEquation(int product)
	{

		int factor = GetRandomFactor(product);
		ValueOperand value1 = new ValueOperand(factor); //1 inclusuive, solution exclusive
		ValueOperand value2 = new ValueOperand(product/factor);
		return new EquationOperand(value1, this, value2);
	}
	
	public override int Apply (Operand operand1, Operand operand2)
	{
		return operand1.GetValue() * operand2.GetValue();
	}
	private int GetRandomFactor(int product)
	{
		Debug.Log (product);
		List<int> factors = new List<int>();
		for (int i = 2; i < product/2+1; i++)
		{
//			Debug.Log (i);

			if (product % i == 0)
			{
//				Debug.Log (i + " = factor"); 
				factors.Add(i);
			}
		}
		int randomNum = Random.Range(0, factors.Count);
	
		Debug.Log(randomNum + "-" + factors.Count);

		return factors[randomNum];
	}
	public static bool isPrime(int value)
	{
		if (value == 2)
			return true;
		for (int i = 2; i < value/2; i++)
		{
			if (value % i == 0)
			{
				return false;
			}
		}
		return true;
	}
	public override int GetRandomValidSolution(int minNum, int maxNum)
	{
		int possibleSolution = Random.Range(minNum, maxNum + 1);
		if (isPrime(possibleSolution))
		{
			return this.GetRandomValidSolution(minNum, maxNum);
		}
		Debug.Log ("Solution is " + possibleSolution);
		return possibleSolution;
	}
}
