using UnityEngine;
using System.Collections;

public class Enemy_Kamikazee : MonoBehaviour {
	
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
	private Done_GameController gameController;
	private int rNum;
	
	void Awake()
	{
		//	target = GameObject.FindGameObjectWithTag ("Player").transform;
		rNum = Random.Range (1, 5);
	}
	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if (target != null){
			distance = Vector3.Distance (transform.position, target.position);
			
			if (distance <= aggroRange || chase) {
				
				chase = true;
				
				if (Physics.Linecast (transform.position, target.position, out hit)) {
					if (hit.collider.tag == "Player" || hit.collider.tag == "PlayerSuperBomb" || hit.collider.tag == "PlayerProjectile" || hit.collider.tag == "PickUp") {
						
						Quaternion targetRotation = Quaternion.LookRotation (target.position - transform.position);
						transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
						transform.Translate (Vector3.forward * Time.deltaTime * speed);
						
					}
				}
			} else {
				transform.LookAt (target);
			}
		}
	}
	
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			
			Instantiate(explosionAnimation, transform.position, transform.rotation);
			Instantiate(explosionSound, transform.position, transform.rotation);
			Instantiate(explosion, transform.position, transform.rotation);
			
			if(rNum == 1)
				Instantiate(dropOnDeath1, transform.position, transform.rotation);
			else if(rNum == 2)
				Instantiate(dropOnDeath2, transform.position, transform.rotation);
			else if(rNum == 3)
				Instantiate(dropOnDeath3, transform.position, transform.rotation);
			
			Destroy(transform.gameObject);
			
		}
	}
	
	
	void OnTriggerEnter (Collider other){
		
		if (other.tag == "PlayerProjectile") {
			
			GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
			
			//get script info from other
			PlayerProjectile playerProjectile = other.GetComponent<PlayerProjectile>();
			
			//subtract other's projectile damage from health
			health = health - playerProjectile.damage;
			
			if(health <= 0f){
				if(rNum == 1)
					Instantiate(dropOnDeath1, transform.position, transform.rotation);
				else if(rNum == 2)
					Instantiate(dropOnDeath2, transform.position, transform.rotation);
				else if(rNum == 3)
					Instantiate(dropOnDeath3, transform.position, transform.rotation);
				
				Instantiate(deathAnimation, transform.position, transform.rotation);
				Instantiate(deathSound, transform.position, transform.rotation);
				//Destroy(transform.gameObject);
				
				gameController.GameOver();
				Application.LoadLevel("GameOver");
				//	gameController.AddScore(scoreValue);
				Destroy (other.gameObject);
				Destroy (gameObject);
			}
			
		}
		
		if (other.tag == "PlayerSuperBomb") {
			
			//get script info from other
			PlayerSuperBomb playerProjectile = other.GetComponent<PlayerSuperBomb>();
			
			//subtract other's projectile damage from health
			health = health - playerProjectile.damage;
			
			if(health <= 0f){
				if(rNum == 1)
					Instantiate(dropOnDeath1, transform.position, transform.rotation);
				else if(rNum == 2)
					Instantiate(dropOnDeath2, transform.position, transform.rotation);
				else if(rNum == 3)
					Instantiate(dropOnDeath3, transform.position, transform.rotation);
				
				Instantiate(deathAnimation, transform.position, transform.rotation);
				//	Destroy(transform.gameObject);
				
				gameController.GameOver();
				Application.LoadLevel("GameOver");
				//	gameController.AddScore(scoreValue);
				Destroy (other.gameObject);
				Destroy (gameObject);
			}
			
		}
	}
}