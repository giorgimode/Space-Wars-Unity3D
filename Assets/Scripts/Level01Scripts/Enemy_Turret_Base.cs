using UnityEngine;
using System.Collections;

public class Enemy_Turret_Base : MonoBehaviour {

	public GameObject deathSound;
	public GameObject deathAnimation;
	public GameObject dropOnDeath1;
	public GameObject dropOnDeath2;
	public GameObject dropOnDeath3;
	private int rNum;

	public float health;

	void Awake(){
		rNum = Random.Range (1, 4);
	}

	 void OnCollisionEnter(Collision other){
		
		if (other.gameObject.tag == "PlayerProjectile") {

			GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
			
			//get script info from other
            PlayerProjectile playerProjectile = other.gameObject.GetComponent<PlayerProjectile>();
			
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

        if (other.gameObject.tag == "PlayerSuperBomb")
        {
			
			//get script info from other
            PlayerSuperBomb playerProjectile = other.gameObject.GetComponent<PlayerSuperBomb>();
			
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
