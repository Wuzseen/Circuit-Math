using UnityEngine;
using System.Collections;

public class SubtractionOperator : Operator {


	public SubtractionOperator()
	{
		symbol = '-';
		nodePath = "SubtractionNode";


	}
	// Use this for initialization
	public override EquationOperand GetRandomEquation(int difference)
	{
		ValueOperand value2 = new ValueOperand(Random.Range(1, difference)); //1 inclusuive, solution exclusive
		ValueOperand value1 = new ValueOperand(difference + value2.GetValue());
		return new EquationOperand(value1, this, value2);
	}
	
	public override int Apply (Operand operand1, Operand operand2)
	{
		return operand1.GetValue() - operand2.GetValue();
	}
}
