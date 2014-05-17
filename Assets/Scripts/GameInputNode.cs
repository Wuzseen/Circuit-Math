using UnityEngine;
using System.Collections;

// Only has 1 output node
public class GameInputNode : GameNode {
	public int inputValue;
	
	// Use this for initialization
	protected override void Start () {
		this.NodeValue = inputValue;	
		label.text = this.NodeValue.ToString();
		base.Start();
	}
}
