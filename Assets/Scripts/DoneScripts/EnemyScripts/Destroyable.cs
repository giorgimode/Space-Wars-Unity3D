using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour {
    public float health=10;
    public GameObject destructionSound;
    public GameObject destructionAnimation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void takeDamage(float damage)
    {
        health -= damage;
 
        if (health <= 0)
        {   //FIX HERE ADD destruction prefabs from lvl1
            Instantiate(destructionSound, transform.position, transform.rotation);
            Instantiate(destructionAnimation, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
