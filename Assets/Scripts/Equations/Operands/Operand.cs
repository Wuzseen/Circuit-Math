using UnityEngine;
using System.Collections;

public abstract class Operand {

	protected int value; 

	public abstract string ToString();
	public abstract int GetValue();
}
