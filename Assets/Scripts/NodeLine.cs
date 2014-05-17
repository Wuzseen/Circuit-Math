using UnityEngine;
using System.Collections;

public class NodeLine : MonoBehaviour {
	public static NodeLine CreateNewLine(Transform a, Transform b, Color c1, Color c2) {
		GameObject o = (GameObject)Instantiate(new GameObject());
		o.layer = 8; // GUI/gamelayer
		NodeLine ret = o.AddComponent<NodeLine>();
		ret.lr = o.AddComponent<LineRenderer>();
		ret.lr.material = new Material (Shader.Find("Particles/Additive"));
		ret.lr.SetColors(c1, c2);

		ret.lr.SetWidth(.02f,.02f);
		ret.A = a;
		ret.B = b;
		return ret;
	}

	Transform _a, _b;
	public Transform A {
		set { _a = value; }
	}

	public Transform B {
		set { _b = value; }
	}

	public LineRenderer lr;

	public NodeLine() {

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(lr != null) {
			lr.SetPosition(0,_a.position);
			lr.SetPosition(1,_b.position);
		}
	}
}
