using UnityEngine;
using System.Collections;

public class AdditionOperator : Operator {

	public AdditionOperator()
	{
		symbol = '+';
		nodePath = "AdditionNode";
		
	}	// Use this for initialization
	public override EquationOperand GetRandomEquation(int sum)
	{
		ValueOperand value1 = new ValueOperand(Random.Range(1, sum)); //1 inclusuive, solution exclusive
		ValueOperand value2 = new ValueOperand(sum - value1.GetValue());
		return new EquationOperand(value1, this, value2);
	}

	public override int Apply (Operand operand1, Operand operand2)
	{
		return operand1.GetValue() + operand2.GetValue();
	}
}
