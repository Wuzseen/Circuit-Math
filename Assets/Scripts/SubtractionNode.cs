using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class SubtractionNode : GameNode {

	// Use this for initialization
	
	protected override void Start () {
		label.text = "-";

		base.Start();
	}

	public override int NodeValue {
		get {
			int difference = inputNodes[0].NodeValue + inputNodes[0].NodeValue; // require at least 2 nodes TODO - make this better
			if (inputNodes.Count < 2) {
				return inputNodes[0].NodeValue;
			}
			foreach(GameNode node in this.inputNodes) {
				difference -= node.NodeValue;
			}
			return difference;
		}
	}
}
