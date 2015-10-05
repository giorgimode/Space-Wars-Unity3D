﻿using UnityEngine;
using System.Collections;

public class Enemy_Kamikazee_for_Boss3 : MonoBehaviour {

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
	
	private Transform target;
	private float distance;
	private RaycastHit hit;
	private bool chase = false;

	private int rNum;
	
	void Awake()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		rNum = Random.Range (1, 4);
	}
	
	// Update is called once per frame
	void Update ()
	{
		distance = Vector3.Distance (transform.position, target.position);
		
		if (distance <= aggroRange || chase) {
			
			chase = true;

			Quaternion targetRotation = Quaternion.LookRotation (target.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
			transform.Translate (Vector3.forward * Time.deltaTime * speed);

		}
		else{
			transform.LookAt(target);
		}
	}
	
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {

			Instantiate(explosionAnimation, transform.position, transform.rotation);
			Instantiate(explosionSound, transform.position, transform.rotation);
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy(transform.gameObject);

			if(rNum == 1)
				Instantiate(dropOnDeath1, transform.position, transform.rotation);
			else if(rNum == 2)
				Instantiate(dropOnDeath2, transform.position, transform.rotation);
			else if(rNum == 3)
				Instantiate(dropOnDeath3, transform.position, transform.rotation);
			
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
				Destroy(transform.gameObject);
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
				Destroy(transform.gameObject);
			}
			
		}
	}
}