using UnityEngine;
using System.Collections;

public class SquareNode : GameNode {
	
	// Use this for initialization
	
	protected override void Start () {
		label.text = "^2";
		base.Start();
	}
	
	public override int NodeValue {
		get {
			if (inputNodes.Count < 1) {
				return 0;
			}

			return inputNodes[0].NodeValue * inputNodes[0].NodeValue;
		}
	}
}
