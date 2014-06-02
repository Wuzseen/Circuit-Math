using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {
	public Mesh mesh;
	public Material material;
	public float childScale = 0.5f;

	public int maxDepth = 4;
	private int depth;
	// Use this for initialization
	void Start () {
		gameObject.AddComponent<MeshFilter>().mesh = mesh;
		gameObject.AddComponent<MeshRenderer>().material = material;
		if(depth < maxDepth) {
			new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.up);
			new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.right);
		}
	}

	private void Initialize(Fractal parent, Vector3 dir) {
		gameObject.layer = parent.gameObject.layer;
		mesh = parent.mesh;
		material = parent.material;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		childScale = parent.childScale;
		transform.parent = parent.transform;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition = dir * (0.5f + 0.5f * childScale);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
