using UnityEngine;
using System.Collections;

public class ShieldSpin : MonoBehaviour {

    public float speed = 100.0f;
    private Vector3 weight;

	// Use this for initialization
	void Start () {
        weight = new Vector3(0.1f, 1.0f, 0.5f);
        weight = weight * speed;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles + (weight * Time.deltaTime));
	}
}
