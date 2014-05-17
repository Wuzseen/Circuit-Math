using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Connections are reciprocal for visual needs
public class NodeConnection {
	public DragNode a;
	public DragNode b;

	public NodeConnection (DragNode _a, DragNode _b) {
		a = _a;
		b = _b;
	}

	public bool IsEqual(NodeConnection other) {
		if(other.a == this.a && other.b == this.b) {
			return true;
		}
		if(other.a == this.b && other.b == this.a) {
			return true;
		}
		return false;
	}

	public bool NodeInConnection(DragNode n) {
		return a == n || b == n;
	}

	public override int GetHashCode() {
		return a.NodeID + b.NodeID;
	}
}


public class NodeConnector : MonoBehaviour {
	private Dictionary<NodeConnection,NodeLine> connections = new Dictionary<NodeConnection, NodeLine>();
	private NodeLine activeLine;
	public GameObject lineRendererPrefab;
//	private List<NodeConnection> connections = new List<NodeConnection>();
	// Use this for initialization
	void Awake () {
		DragNode.OnConnectionMade += NewConnection;
		DragNode.OnConnectionStart += StartConnection;
		DragNode.OnPrematureEnd += EndLineDrag;
	}

	bool CompatibleNodes(DragNode a, DragNode b) {
		if(a.ParentNode == b.ParentNode) {
			return false;
		}
		if(a.IsInput == true && b.IsInput == false) {
			return true;
		}
		if(a.IsInput == false && b.IsInput == true) {
			return true;
		}
		return false;
	}

	void EndLineDrag(NodeConnectionArgs args) {
		if(activeLine != null) {
			Destroy (activeLine.gameObject);
		}
	}

	void StartConnection(NodeConnectionArgs args) {
		List<NodeConnection> toUnregsiter = new List<NodeConnection>();
		foreach(NodeConnection nc in connections.Keys) {
			if(nc.NodeInConnection(args.StartNode)) {
				toUnregsiter.Add(nc);
			}
		}
		foreach(NodeConnection nc in toUnregsiter) {
			UnregsiterConnection(nc);
		}
		if(activeLine != null) {
			Destroy (activeLine.gameObject);
		}
		activeLine = NodeLine.CreateNewLine(args.StartNode.transform,UICursor.instance.transform,args.StartNode.ParentNode.NodeColor,Color.green);
	}

	public void NewConnection(NodeConnectionArgs args) {
		if(CompatibleNodes(args.StartNode,args.EndNode)) {
			if(args.StartNode.IsInput == false) {
				args.EndNode.ParentNode.AddInputNode(args.StartNode.ParentNode);
			} else {
				args.StartNode.ParentNode.AddInputNode(args.EndNode.ParentNode);
			}
			RegisterConnection(new NodeConnection(args.StartNode,args.EndNode));
		} else {
			print ("Not connectible, input -> output and output -> input only between two unique nodes.");
		}
	}

	public void RegisterConnection(NodeConnection nc) {
		if(activeLine != null) {
			Destroy (activeLine.gameObject);
		}

		NodeConnection ncToBreak = null;
		foreach(NodeConnection nodeConnect in connections.Keys) {
			if(nodeConnect.NodeInConnection(nc.a) || nodeConnect.NodeInConnection(nc.b))
				ncToBreak = nodeConnect;
		}
		if(ncToBreak != null)
			UnregsiterConnection(ncToBreak);
		if(connections.ContainsKey(nc)) {
			return;
		}

		NodeLine lr = NodeLine.CreateNewLine(nc.a.transform,nc.b.transform,nc.a.ParentNode.NodeColor,nc.b.ParentNode.NodeColor);
		connections.Add(nc,lr);
	}

	public void UnregsiterConnection(NodeConnection nc) {
		if(activeLine != null) {
			Destroy (activeLine.gameObject);
		}
		NodeLine lr = connections[nc];
		if(nc.a.IsInput == false) {
			nc.b.ParentNode.RemoveInputNode(nc.a.ParentNode);
		} else {
			nc.a.ParentNode.RemoveInputNode(nc.b.ParentNode);
		}
		connections.Remove(nc);
		Destroy (lr.gameObject);
	}

	// Update is called once per frame
	void OnPress(bool isDown) {
	}

	void OnDrop() {
		if(activeLine != null) {
			Destroy (activeLine.gameObject);
		}
	}
}
