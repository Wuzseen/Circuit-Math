using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CueHelper : MonoBehaviour {
	public UISprite hand;
	public float sleepTime = 10f;
	private float timer = 0f;
	// Use this for initialization
	void Start () {
		StartCoroutine("WaitForTip");
		hand.alpha = 0f;
		DragNode.OnConnectionMade += InputGathered;
	}

	void InputGathered(NodeConnectionArgs args) {
		timer = 0f;
	}

	IEnumerator WaitForTip() {
		while(true) {
			while(timer < sleepTime) {
				timer += Time.deltaTime;
				yield return 0;
			}
			GameInputNode[] giNodes = GameObject.FindObjectsOfType(typeof(GameInputNode)) as GameInputNode[];
			GameObject[] opNodes = GameObject.FindGameObjectsWithTag("OperatorNode");
			GameObject randomOpGO = opNodes[Random.Range(0,opNodes.Length)];
			GameInputNode giNode = giNodes[Random.Range(0,giNodes.Length)];
			GameNode opNode = randomOpGO.GetComponent<GameNode>();
			Transform a = giNode.outputNodeParent.transform.GetChild(Random.Range(0,giNode.outputNodeParent.transform.childCount));
			Transform b = opNode.inputNodeParent.transform.GetChild(Random.Range(0,opNode.inputNodeParent.transform.childCount));
			transform.position = a.position;
			Go.to (hand,.3f,new GoTweenConfig().floatProp("alpha",1f));
			yield return new WaitForSeconds(.2f);
			transform.positionTo(3f,b.position);
			yield return new WaitForSeconds(3f);
			Go.to (hand,.3f,new GoTweenConfig().floatProp("alpha",0f));
			timer = 0f;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
