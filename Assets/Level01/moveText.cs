using UnityEngine;
using System.Collections;

public class moveText : MonoBehaviour {
	private Texture myTexture;
	private Vector2 rate = new Vector2(1.2f, 1.25f);
	Vector2 offset = Vector2.zero;
	// Use this for initialization
	void Start () {
		myTexture = gameObject.GetComponent<Renderer>().material.mainTexture;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		offset += (rate * Time.deltaTime);
		if (GetComponent<Renderer>().enabled) {
			GetComponent<Renderer>().materials[0].SetTextureOffset("BaseShield", offset);	
		}
	}
}
