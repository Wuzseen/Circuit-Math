using UnityEngine;
using System.Collections;

public class EquationOperand : Operand {
	public Operand operand1; 
	public Operator _operator;
	public Operand operand2;
	// Use this for initialization

	public EquationOperand(Operand _operand1, Operator op, Operand _operand2)
	{
		operand1 = _operand1;
		_operator = op;
		operand2 = _operand2;
	}
	public override string ToString()
	{
		return "(" + operand1.ToString() + " " + _operator.symbol + " " + operand2.ToString() + ")";
	}
	public override int GetValue()
	{
		return _operator.Apply(operand1, operand2);
	}
}
