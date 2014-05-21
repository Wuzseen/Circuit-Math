using UnityEngine;
using System.Collections;

public class ValueOperand : Operand {
	
	public ValueOperand(int _value)
	{
		value = _value;
	}
	public override string ToString()
	{
		return value.ToString();
	}
	public override int GetValue()
	{
		return value;
	}

}
