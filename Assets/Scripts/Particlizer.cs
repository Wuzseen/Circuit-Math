using UnityEngine;
using System.Collections;

public class Particlizer : MonoBehaviour {
	public static Particlizer Instance;
	public GameObject sparkDrop;
	// Use this for initialization
	void Awake () {
		Instance = this;
	}


	public void Shock(Vector3 position) {
		Destroy((GameObject)Instantiate(sparkDrop,position,Quaternion.identity),2f);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
