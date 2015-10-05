using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour {
	public float health;
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private Done_GameController gameController;
	private PlayerController scoreController;
	// Use this for initialization
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
	void Update () {
	
	}

	void OnTriggerEnter (Collider other){
		
		if (other.tag == "PlayerProjectile") {
			
			//get script info from other
			PlayerProjectile playerProjectile = other.GetComponent<PlayerProjectile>();
			
			//subtract other's projectile damage from health
			health = health - playerProjectile.damage;
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			if(health <= 0f)
			{
				Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
				//gameController.GameOver();
				Application.LoadLevel("GameOver");
				scoreController.AddScore(scoreValue);
				Destroy (other.gameObject);
				Destroy (gameObject);
			}
			
		}
	}
}
