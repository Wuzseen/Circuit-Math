using UnityEngine;
using System.Collections;

public abstract class Operator {

	public char symbol;
	public string nodePath;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public Operator()
	{

	}
	public virtual int GetRandomValidSolution(int minNum, int maxNum)
	{
		return Random.Range(minNum, maxNum+1);
	}

	public abstract EquationOperand GetRandomEquation(int solution);
	public abstract int Apply(Operand operand1, Operand operand2);

}
