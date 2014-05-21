using UnityEngine;
using System.Collections;

public abstract class Operator : MonoBehaviour {

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

	public abstract EquationOperand GetRandomEquation(int solution);
	public abstract int Apply(Operand operand1, Operand operand2);

}
