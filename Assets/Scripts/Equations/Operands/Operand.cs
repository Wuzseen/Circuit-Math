using UnityEngine;
using System.Collections;

public abstract class Operand {

	protected int value; 

	public new abstract string ToString();
	public abstract int GetValue();
}
