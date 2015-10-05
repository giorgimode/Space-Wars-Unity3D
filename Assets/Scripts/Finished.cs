using UnityEngine;
using System.Collections;

public class Finished : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider Others)
	{
		if (Others.tag == "Finished")

		{
			Application.LoadLevel ("GameFinished");

		}
		}
}
