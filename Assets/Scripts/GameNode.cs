using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum NodeType {
	Integer,
	Addition,
	Subtraction,
	Multiplication,
	Goal
}

[RequireComponent (typeof(UILabel))]
public class GameNode : MonoBehaviour {
	public bool Draggable;
	public Color NodeColor;
	public NodeType NodeType;
	
	public Transform inputNodeParent;
	protected int activeInputNodeCount;
	protected List<DragNode> inputDragNodes;
	protected List<GameNode> inputNodes = new List<GameNode>();

	public Transform outputNodeParent;
	protected int activeOutputNodeCount;
	protected List<DragNode> outputDragNodes;

	private UIDragDropItem dragScript;
	public UILabel label;
	// Use this for initialization
	protected virtual void Start () {
		dragScript = (UIDragDropItem)GetComponent(typeof(UIDragDropItem));
		DragToggle(Draggable);
		if(outputNodeParent != null) {
			outputDragNodes = new List<DragNode>(outputNodeParent.GetComponentsInChildren<DragNode>());
		}
		if(inputNodeParent != null) {
			inputDragNodes = new List<DragNode>(inputNodeParent.GetComponentsInChildren<DragNode>());
		}
		NodeColor =	this.GetComponent<UISprite>().color;
		if(outputDragNodes != null) {
			foreach(DragNode n in outputDragNodes) {
				n.ParentNode = this;
				n.IsInput = false;
				n.Sprite.color = NodeColor;
			}
		}
		if(inputDragNodes != null) {
			foreach(DragNode n in inputDragNodes) {
				n.ParentNode = this;
				n.IsInput = true;
				n.Sprite.color = NodeColor;
			}
		}
	}

	public void AddInputNode(GameNode value) {
		print ("ADDING CONNECTION");
		if(inputNodes.Contains(value) == false) {
			inputNodes.Add(value);
		}
	}

	public void RemoveInputNode(GameNode node) {
		inputNodes.Remove(node);
	}

	public void DragToggle() {
		DragToggle(!dragScript.enabled);
	}

	public void DragToggle(bool onOrOff) {
		dragScript.enabled = onOrOff;
	}

	private int nodeValue;
	public virtual int NodeValue {
		get {
			return nodeValue;
		}
		set {
			nodeValue = value;
		}
	}

	void OnClick () {
		print (this.gameObject.name);
	}

//	void OnDrag (Vector2 delta) {
//	}
}
