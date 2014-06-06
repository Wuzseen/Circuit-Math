using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeLine : MonoBehaviour {
	private float nodeZpos = .3f;
	private bool betweenOperators = false;
	public static NodeLine CreateNewLine(Transform a, Transform b, Color c1, Color c2) {
		GameObject o = new GameObject();
		o.layer = 8; // GUI/gamelayer
		NodeLine ret = o.AddComponent<NodeLine>();
		ret.lr = o.AddComponent<LineRenderer>();
		ret.lr.material = new Material (Shader.Find("Particles/Additive"));
		ret.lr.SetColors(c1, c2);

		if (a.parent.name == "OutputNodes" && a.parent.parent.name != "GameInputNode") {
			ret.betweenOperators = true;
		} else if (b.parent.name == "OutputNodes" && b.parent.parent.name != "GameInputNode") {
			ret.betweenOperators = true;
		} else {
			ret.betweenOperators = false;
		}

		ret.lr.SetWidth(.02f,.02f);
		ret.A = a;
		ret.B = b;
		return ret;
	}

	Transform _a, _b;
	public Transform A {
		get { return _a; }
		set { _a = value; }
	}

	public Transform B {
		get { return _b; }
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
			p1.z = nodeZpos;
			Vector3 p2 = _b.position;
			p2.z = nodeZpos;
			if(A.gameObject.activeInHierarchy == false || B.gameObject.activeInHierarchy == false) {
				Destroy(this.gameObject);
				return;
			}
			List<Vector3> points = new List<Vector3>();
			points.Add(p1);
			if (Mathf.Abs(p2.x - p1.x) > 0.01f && Mathf.Abs(p2.y - p1.y) > 0.01f) {
				if (betweenOperators) {
					float center = (p2.y - p1.y)/2 + p1.y;
					float thingyWidth = .1f;
					float dist = Mathf.Abs(p2.x - p1.x);
					thingyWidth = Mathf.Clamp(dist/6,0.1f,.1f);
					float yMinus = center < p1.y ? center - thingyWidth : center + thingyWidth;
					float yPlus = center > p2.y ? center + thingyWidth : center - thingyWidth;
					float xLeft = p2.x > p1.x ? p1.x + thingyWidth : p1.x - thingyWidth;
					float xRight = p2.x > p1.x ? p2.x - thingyWidth : p2.x + thingyWidth;

					if (p2.y > center && p2.y < yMinus) {
						yMinus = p2.y - Mathf.Abs(p2.y - center) / 2;
						yPlus = p1.y + Mathf.Abs(p1.y - center) / 2;
					} else if (p2.y < center && p2.y > yMinus) {
						yMinus = p2.y + Mathf.Abs(p2.y - center) / 2;
						yPlus = p1.y - Mathf.Abs(p1.y - center) / 2;
					}
					
					points.Add(new Vector3(p1.x,yPlus,nodeZpos));
					Vector3 yTopV = new Vector3(xLeft,center,nodeZpos);
					Vector3 yBotV = new Vector3(xRight,center,nodeZpos);
					points.Add(yTopV);
					points.Add(yBotV);
					points.Add(new Vector3(p2.x,yMinus,nodeZpos));
				} else if (!betweenOperators) {
					float center = (p2.x - p1.x)/2 + p1.x;
					float thingyWidth = .1f;
					float dist = Mathf.Abs(p2.y - p1.y);
					thingyWidth = Mathf.Clamp(dist/6,.01f,.1f);
					float xMinus = center > p1.x ? center - thingyWidth : center + thingyWidth;
					float xPlus = center < p2.x ? center + thingyWidth : center - thingyWidth;
					float yTop = p2.y > p1.y ? p1.y + thingyWidth : p1.y - thingyWidth;
					float yBot = p2.y > p1.y ? p2.y - thingyWidth : p2.y + thingyWidth;
//					float xMinus = Mathf.Lerp(p1.x,center,.85f);
//					float xPlus = Mathf.Lerp(center,p2.x,.15f);
//					float yTop = Mathf.Lerp (p1.y,p2.y,.15f);
//					float yBot = Mathf.Lerp (p1.y,p2.y,.85f);
					// Need points at p1x,p1y xMinus,p1y yTop,center yBot,center xPlus,p2y p2x,p2y
//					points.Add(new Vector3(p1.x,p1.y,0f));
					
					if (p2.x > center && p2.x < xPlus) {
						xPlus = p2.x - Mathf.Abs(p2.x - center) / 2;
						xMinus = p1.x + Mathf.Abs(p1.x - center) / 2;
					} else if (p2.x < center && p2.x > xPlus) {
						xPlus = p2.x + Mathf.Abs(p2.x - center) / 2;
						xMinus = p1.x - Mathf.Abs(p1.x - center) / 2;
					}

					points.Add(new Vector3(xMinus,p1.y,nodeZpos));
					Vector3 yTopV = new Vector3(center,yTop,nodeZpos);
					Vector3 yBotV = new Vector3(center,yBot,nodeZpos);
					points.Add(yTopV);
					points.Add(yBotV);
					points.Add(new Vector3(xPlus,p2.y,nodeZpos));
//					points.Add(new Vector3(p2.x,p2.y,0f));
//					points.Add(new Vector3(.98f*(p2.x - p1.x)/2 + p1.x,p1.y,0));
//					points.Add(new Vector3(center, p1.y, 0));
//					float t1 = p1.y + (p2.y - p1.y) * .02f; // Helps avoid the bill boarding issues, not pretty code, but prettier lines
//					float t2 = p1.y + (p2.y - p1.y) * .98f;
//					points.Add(new Vector3(center,t1,0));
//					points.Add(new Vector3(center,t2,0));
//					points.Add(new Vector3(center, p2.y, 0));
				}
			}
			points.Add(p2);
			lr.SetVertexCount(points.Count);

			for (int i = 0; i < points.Count; i++) {
				lr.SetPosition(i, points[i]);
			}
		}
	}
}
