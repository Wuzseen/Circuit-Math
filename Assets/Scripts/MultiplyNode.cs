using UnityEngine;
using System.Collections;

public class MultiplyNode : GameNode {
	
	// Use this for initialization
	
	protected override void Start () {
		this.tag = "OperatorNode";
		label.text = "X";
		base.Start();
	}
	
	public override int NodeValue {
		get {
			int product = 1; // require at least 2 nodes
			if (inputNodes.Count < 2) {
				return 0;
			}
			foreach(GameNode node in this.inputNodes) {
				product *= node.NodeValue;
			}
			return product;
		}
	}
}
