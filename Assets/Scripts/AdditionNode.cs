using UnityEngine;
using System.Collections;

public class AdditionNode : GameNode {

	// Use this for initialization
	
	protected override void Start () {
		label.text = "+";
		base.Start();
	}

	public override int NodeValue {
		get {
			int sum = 0;
			foreach(GameNode node in this.inputNodes) {
				sum += node.NodeValue;
			}
			return sum;
		}
	}
}
