using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class AdditionNode : GameNode {

	// Use this for initialization
	
	protected override void Start () {
		this.tag = "OperatorNode";
		label.text = "+";
		base.Start();
	}

	public override int NodeValue {
		get {
			int sum = 0; // require at least 2 nodes
			if (inputNodes.Count < 2) {
				return 0;
			}
			foreach(GameNode node in this.inputNodes) {
				sum += node.NodeValue;
			}
			return sum;
		}
	}
}
