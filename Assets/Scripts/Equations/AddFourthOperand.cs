using UnityEngine;
using System.Collections;

public class AddFourthOperand : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Randomizer randomizer = GameObject.FindObjectOfType<Randomizer>();
		if (randomizer.maxNumOperators <= 3)
		{
			Debug.Log ("Here");
			gameObject.SetActive(false);
		}
		else{
			Debug.Log ("greater than 3");
			gameObject.SetActive(true);
			randomizer.inputNodes.Add(GetComponent<GameInputNode>());
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}