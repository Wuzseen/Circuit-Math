using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FractalColor {
	public Color startColor;
	public Color currentColor;
	public Color endColor;
	public bool lerping = false;
	public bool on = false;

	public FractalColor(Color c1, Color c2) {
		startColor = c1;
		currentColor = startColor;
		endColor = c2;
		on = false;
	}
}

public class Fractal : MonoBehaviour {
	public static Fractal FractalMaster;
	public static int usedColor = 0;
	public Dictionary<Material,FractalColor> fractalColors;
	public float RotateSpeed = 15f;
	public Mesh mesh;
	public Material material;
	public float childScale = 0.5f;
	public float maxTwist;
	public float spawnProbability = 0.8f;
	public int maxDepth = 4;
	private int depth;
	
	private static Vector3[] childDirections = {
		Vector3.up,
		Vector3.right,
		Vector3.left,
		Vector3.down,
//		Vector3.forward,
		Vector3.back
	};
	
	private static Quaternion[] childOrientations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 0f, -90f),
		Quaternion.Euler(0f, 0f, 90f),
		Quaternion.identity,
//		Quaternion.Euler(90f,0f,0f),
		Quaternion.Euler(-90f,0f,0f)
	};

	private Material[] materials;
	
	private void InitializeMaterials () {
		fractalColors = new Dictionary<Material, FractalColor>();
		Color[] colors = new Color[] {
			new Color32(0,16,64,255),
			new Color32(0,64,255,255),
			new Color32(0,48,191,255),
			new Color32(0,58,229,255),
			new Color32(0,32,127,255),
			new Color32(4,70,127,255)
		};
		materials = new Material[colors.Length];
		for (int i = 0; i < colors.Length; i++) {
			materials[i] = new Material(material);
			materials[i].color = colors[Random.Range(0,colors.Length)];
			fractalColors.Add(materials[i],new FractalColor(materials[i].color,new Color32(76,200,20,255)));
		}
	}

	private void Start() {
		Create ();
	}

	public void Create () {
		if (materials == null) {
			InitializeMaterials();
		}
		if(depth > 0)
			transform.Rotate(Random.Range (-maxTwist, maxTwist), 0f, 0f);
		gameObject.AddComponent<MeshFilter>().mesh = mesh;
		//		gameObject.AddComponent<MeshRenderer>().material = materials[Random.Range(0,materials.Length)];
				gameObject.AddComponent<MeshRenderer>().material = materials[usedColor];
		if(depth < maxDepth) {
			for (int i = 0; i < childDirections.Length; i++) {
				if(Random.value < spawnProbability || depth == 0) {
					new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, i);
				}
			}
		}
		if(depth == 0) {
//			StartCoroutine("InputHandler");			
			FractalMaster = this;
		}
		usedColor ++;
		if(usedColor >= materials.Length) {
			usedColor = 0;
		}
	}

	private void Initialize(Fractal parent, int childIndex) {
		fractalColors = parent.fractalColors;
		gameObject.layer = parent.gameObject.layer;
		mesh = parent.mesh;
		RotateSpeed = parent.RotateSpeed;
		spawnProbability = parent.spawnProbability;
		materials = parent.materials;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		maxTwist = parent.maxTwist;
		childScale = parent.childScale;
		transform.parent = parent.transform;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
		transform.localRotation = childOrientations[childIndex];
	}

	private void Update () {
		transform.Rotate(0f, RotateSpeed * Time.deltaTime, depth == 0 ? RotateSpeed * Time.deltaTime : 0f);
	}

	IEnumerator InputHandler() {
		while(true) {
			if(Input.GetKeyDown(KeyCode.Space)) {
				HighlightBG(1);
			}
			yield return 0;
		}
	}

	public void RandomBGChange() {
		HighlightBG(Random.Range(0,materials.Length));
	}

	public void TurnAllOn() {
		foreach(Material m in materials) {
			FractalColor fc = fractalColors[m];
			fc.on = true;
			Go.to (m,.6f, new GoTweenConfig().materialColor(fc.endColor));
		}
	}
	
	public void TurnAllOff() {
		foreach(Material m in materials) {
			FractalColor fc = fractalColors[m];
			fc.on = false;
			Go.to (m,.6f, new GoTweenConfig().materialColor(fc.startColor));
		}
	}

	public void HighlightBG(int index) {
		FractalColor fc = fractalColors[materials[index]];
		if(fc.on) {
			Go.to (materials[index],.6f, new GoTweenConfig().materialColor(fc.startColor));
			fc.on = false;
		} else {
			Go.to (materials[index],.6f, new GoTweenConfig().materialColor(fc.endColor));
			fc.on = true;
		}
	}
}
