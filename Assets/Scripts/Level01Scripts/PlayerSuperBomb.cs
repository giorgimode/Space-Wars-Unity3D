using UnityEngine;
using System.Collections;

public class PlayerSuperBomb : MonoBehaviour {
	
	public int damage;
	public float duration;

	private Transform player;
	
	IEnumerator SuperBombDuration()
	{
		yield return new WaitForSeconds (duration);
		Destroy (transform.gameObject);
	}
	
	void Start()
	{
		StartCoroutine (SuperBombDuration ());
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update(){
		transform.position = player.position;
		transform.rotation = player.rotation;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "EnemyProjectile") {
			Destroy(other.transform.gameObject);
		}
	}

}
