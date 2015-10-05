using UnityEngine;
using System.Collections;

public class Level3Transition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Application.LoadLevel("Level3");
		}
	}
}
