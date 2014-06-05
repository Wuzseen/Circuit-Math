using UnityEngine;
using System;
using System.Collections;

public class DivisionNode : GameNode {
	
	// Use this for initialization
	
	protected override void Start () {
		this.tag = "OperatorNode";
		label.text = "/";

		base.Start();
	}
	
	public override int NodeValue {
		get {
			if (inputNodes.Count < 1) {
				return 0;
			}

			if (inputNodes.Count < 2) {
				return inputNodes[0].NodeValue;
			}

			// get all the inputs and put them in an array
			int[] inputs = new int[inputNodes.Count];
			for (int i = 0; i < inputNodes.Count; i++) {
				inputs[i] = inputNodes[i].NodeValue;
			}
			Array.Sort(inputs);
			int quotient = inputs[inputs.Length - 1];
			for (int i = inputs.Length - 2; i >= 0; i--) {
				quotient /= inputs[i];
			}

			return quotient;
		}
	}
}