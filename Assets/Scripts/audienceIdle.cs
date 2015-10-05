using UnityEngine;
using System.Collections;

public class audienceIdle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Animation>().CrossFade ("idle", 0.2f);
	}
}
