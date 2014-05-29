using UnityEngine;
using System.Collections;

public class DivisionNode : GameNode {
	
	// Use this for initialization
	
	protected override void Start () {
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

			// sort the inputs in descending order
			bool done = false;
			while (!done) {
				done = true;
				for (int i = 0; i < inputs.Length - 1; i++) {
					if (inputs[i] < inputs[i + 1]) {
						int temp = inputs[i];
						inputs[i] = inputs[i + 1];
						inputs[i + 1] = temp;
						done = false;
					}
				}
			}

			// divide by each input, discard remainders
			int quotient = inputs[0];
			for (int i = 1; i < inputs.Length; i++) {
				quotient = quotient / inputs[i];
			}

			return quotient;
		}
	}
}