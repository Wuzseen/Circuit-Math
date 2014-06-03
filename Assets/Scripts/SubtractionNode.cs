using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class SubtractionNode : GameNode {

	// Use this for initialization
	
	protected override void Start () {
		this.tag = "OperatorNode";
		label.text = "-";

		base.Start();
	}

	public override int NodeValue {
		get {
			if(inputNodes.Count != 2) {
				print ("BADNESS");
				return -1;
			}
			int difference = Mathf.Abs(inputNodes[0].NodeValue - inputNodes[1].NodeValue); // require at least 2 nodes TODO - make this better
			if (inputNodes.Count < 2) {
				return inputNodes[0].NodeValue;
			}
			return difference;
		}
	}
}
