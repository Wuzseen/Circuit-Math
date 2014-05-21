using UnityEngine;
using System.Collections;

public class Equation : MonoBehaviour {

	public int solution;
	public EquationOperand operand;
	// Use this for initialization
	public Equation(int _solution, EquationOperand _operand)
	{
		solution = _solution;
		operand = _operand;
	}
}
