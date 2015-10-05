using UnityEngine;
using System.Collections;

public class enemy_kamikaze_shooter : MonoBehaviour {
	
	public GameObject explosionSound;
	public GameObject explosion;
	public GameObject explosionAnimation;
	
	public GameObject deathSound;
	public GameObject deathAnimation;
	public GameObject dropOnDeath1;
	public GameObject dropOnDeath2;
	public GameObject dropOnDeath3;
	
	public float health;
	public float aggroRange;
	public float speed;
	public float rotSpeed;
	
	public Transform target;
	private float distance;
	private RaycastHit hit;
	private bool chase = false;
	
	private int rNum;
	
	
	void Awake()
	{
		//	target = GameObject.FindGameObjectWithTag ("Player").transform;
		rNum = Random.Range (1, 4);
	}
	
	// Update is called once per frame
	void Update ()
	{ distance = Vector3.Distance (transform.position, target.position);

		if (target != null && distance <= aggroRange || chase) {
			Quaternion targetRotation = Quaternion.LookRotation (target.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
			transform.Translate (Vector3.forward * Time.deltaTime * speed);
		}
	}
	
	/*
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			//Destroy(transform.gameObject);
		}
	}
	*/
	
	void OnTriggerEnter (Collider other){
		
		if (other.tag == "PlayerProjectile") {
			
			//get script info from other
			PlayerProjectile playerProjectile = other.GetComponent<PlayerProjectile>();
			
			//subtract other's projectile damage from health
			health = health - playerProjectile.damage;
			
			if(health <= 0f)
				Destroy(transform.gameObject);
			
		}

        if (other.tag == "Obstacle")
        {

            //get script info from other
            Obstacle Projectile = other.GetComponent<Obstacle>();

            //subtract other's projectile damage from health
            health = health - Projectile.damage;

            if (health <= 0f)
                Destroy(transform.gameObject);

        }
	}
}