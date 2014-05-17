using UnityEngine;
using System.Collections;

// A goal bucket is just like any other node, but it only has 1 input.
public class GoalNode : GameNode {
	public int GoalValue;

	// Use this for initialization
	
	// Update is called once per frame
	
	protected override void Start () {
		label.text = GoalValue.ToString();
		base.Start();
		Game.Instance.AddGoalBucket(this);
	}
	
	public override int NodeValue {
		get {
			int sum = 0; // require at least 2 nodes
			foreach(GameNode node in this.inputNodes) {
				sum += node.NodeValue;
			}
			print (sum);
			if(sum == GoalValue) {
				return 1;
			}
			return 0;
		}
	}
}
