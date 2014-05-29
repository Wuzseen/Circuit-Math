using UnityEngine;
using System.Collections;

public class DivisionOperator : Operator {
	
	public DivisionOperator()
	{
		symbol = '/';
		nodePath = "DivisionNode";
		
	}	// Use this for initialization
	public override EquationOperand GetRandomEquation(int quotient)
	{
		int divisor = 0;
		do {
			divisor = Random.Range(2, 51); // cap divisor at 50, and don't allow divide by 1
		} while (divisor * quotient > 200); // cap dividend at 200
		ValueOperand value1 = new ValueOperand(divisor);
		ValueOperand value2 = new ValueOperand(quotient * value1.GetValue());
		return new EquationOperand(value2, this, value1);
	}
	
	public override int Apply (Operand operand1, Operand operand2)
	{
		return operand1.GetValue() / operand2.GetValue();
	}
}
