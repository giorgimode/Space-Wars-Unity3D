using UnityEngine;
using System.Collections;

public class PlayerProjectile : MonoBehaviour {

	public int damage;
	public float speed;
	public float maxDistance;

	private Transform playerInitial;
	private float distance;
	
	void Start ()
    {   // FIX ADD DESTROY BY BOUNDARY/TIME
		playerInitial = GameObject.FindGameObjectWithTag ("Player").transform;
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}

	void Update(){
		if (playerInitial != null) {
			distance = Vector3.Distance (transform.position, playerInitial.position);
			if (distance > maxDistance)
				Destroy (transform.gameObject);
		}
	}

    void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.tag == "Player") {
		}
        else if (other.gameObject.tag == "PlayerProjectile")
        {
		}
        else if (other.gameObject.tag == "PlayerSuperBomb")
        {
		}
        else if (other.gameObject.tag == "CheckPoint")
        {
		}
        else if (other.gameObject.tag == "PickUp")
        {
		}
        
        else if (other.gameObject.tag == "FinalBattle")
        {
        }
        else if (other.gameObject.tag == "WinPoint")
        {
        }
		else {
			//transform.gameObject.renderer.enabled = false;
			Destroy (transform.gameObject);
		}
	}
}
