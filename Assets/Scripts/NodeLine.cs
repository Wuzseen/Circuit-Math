using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeLine : MonoBehaviour {
	public static NodeLine CreateNewLine(Transform a, Transform b, Color c1, Color c2) {
		GameObject o = new GameObject();
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
			if(_a == null || _b == null)
				return;
			Vector3 p1 = _a.position;
			p1.z = 0;
			Vector3 p2 = _b.position;
			p2.z = 0;

			List<Vector3> points = new List<Vector3>();
			points.Add(p1);
			if (Mathf.Abs(p2.y - p1.y) > 0.01f) {
				float x = (p2.x - p1.x)/2 + p1.x;
				points.Add(new Vector3(.98f*(p2.x - p1.x)/2 + p1.x,p1.y,0));
				points.Add(new Vector3(x, p1.y, 0));
				float t1 = p1.y + (p2.y - p1.y) * .02f; // Helps avoid the bill boarding issues, not pretty code, but prettier lines
				float t2 = p1.y + (p2.y - p1.y) * .98f;
				points.Add(new Vector3(x,t1,0));
				points.Add(new Vector3(x,t2,0));
				points.Add(new Vector3(x, p2.y, 0));
			}
			points.Add(p2);
			lr.SetVertexCount(points.Count);

			for (int i = 0; i < points.Count; i++) {
				lr.SetPosition(i, points[i]);
			}
		}
	}
}
