using UnityEngine;
using System.Collections;

public class NodeConnectionArgs {
	public DragNode StartNode;
	public DragNode EndNode;

	public NodeConnectionArgs(DragNode _startNode, DragNode _endNode) {
		StartNode = _startNode;
		EndNode = _endNode;
	}
}

[RequireComponent (typeof(SphereCollider))]
public class DragNode : MonoBehaviour {
	private static int nodeIDCount;
	public int NodeID; // used for hashing
	private GameNode parentNode;
	public bool IsInput; // false = output node
	public GameNode ParentNode {
		get {
			return parentNode;
		}
		set {
			parentNode = value;
		}
	}

	private UISprite sprite;
	public UISprite Sprite {
		get {
			if(sprite == null) {
				sprite = this.GetComponent<UISprite>();
			}
			return sprite;
		}
	}

	public delegate void NodeConnectionEventHandler(NodeConnectionArgs args);
	public static event NodeConnectionEventHandler OnConnectionMade;
	public static event NodeConnectionEventHandler OnConnectionStart;
	// Use this for initialization

	void Start () {
		NodeID = nodeIDCount;
		nodeIDCount++;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPress (bool isDown) {
		if(isDown) {
			if(OnConnectionStart != null) {
				OnConnectionStart(new NodeConnectionArgs(this,null));
			}
		}
	}

	void OnDrop (GameObject drag) {
		if(OnConnectionMade != null) {
			DragNode other = (DragNode)drag.GetComponent(typeof(DragNode));
			if(other != null && other != this) {
				OnConnectionMade(new NodeConnectionArgs(other,this));
			}
		}
	}
}
