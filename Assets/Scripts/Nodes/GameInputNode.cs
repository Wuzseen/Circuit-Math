using UnityEngine;
using System.Collections;

// Only has 1 output node
public class GameInputNode : GameNode {
	public int inputValue;
	
	// Use this for initialization
	protected override void Start () {
		SetInputValue(inputValue);
		base.Start();
	}

	public void SetInputValue(int value)
	{
		inputValue = value;
		this.NodeValue = inputValue;
		label.text = NodeValue.ToString();


	}
}
