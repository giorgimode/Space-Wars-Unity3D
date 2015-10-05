using UnityEngine;
using System.Collections;

public class Enemy_Kamikazee_Explosion : MonoBehaviour {

	public float damage;
	public float duration;

	IEnumerator explosionDuration()
	{
		yield return new WaitForSeconds (duration);
		Destroy (transform.gameObject);
	}

	void Start ()
	{
		StartCoroutine (explosionDuration ());
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			Destroy (transform.gameObject);
		}
	}
}
