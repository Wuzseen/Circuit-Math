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

//[RequireComponent (typeof(SphereCollider))]
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
	public UISprite NodeSprite {
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
	public static event NodeConnectionEventHandler OnPrematureEnd;
	private TweenScale scaleTween;
	// Use this for initialization

	void Start () {
//		((SphereCollider)this.collider).radius = 22f;
		((BoxCollider)this.collider).size = new Vector3(66f,39f,.3f);
		scaleTween = GetComponent<TweenScale>();
		scaleTween.to = this.transform.localScale * 1.2f;
		scaleTween.duration = .3f;
		scaleTween.enabled = false;
		NodeID = nodeIDCount;
		nodeIDCount++;
		this.NodeSprite.name = "nodeBG";
	}

	void OnPress (bool isDown) {
		if(isDown) {
			if(OnConnectionStart != null) {
				OnConnectionStart(new NodeConnectionArgs(this,null));
				scaleTween.PlayForward();
			}
		} else {
			if(OnPrematureEnd != null) {
				OnPrematureEnd(new NodeConnectionArgs(this,null));
			}
		}
	}

	public void ResetScale() {
		scaleTween.PlayReverse();
	}

	void OnDrop (GameObject drag) {
		if(OnConnectionMade != null) {
			DragNode other = (DragNode)drag.GetComponent(typeof(DragNode));
			if(other != null && other != this) {
				OnConnectionMade(new NodeConnectionArgs(other,this));
				SoundManager.PlaySFX(SoundManager.LoadFromGroup("ConnectionSounds"));
				scaleTween.PlayForward();
				Particlizer.Instance.Shock(transform.position);
			}
		}
	}
}
