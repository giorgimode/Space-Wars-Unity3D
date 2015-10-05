using UnityEngine;
using System.Collections;

public class PickUps : MonoBehaviour {

	public GameObject pickUpSound;

	public int health;
	public int flameThrower;
	public int lightningGun;
	public int missiles;
	public int heatMissiles;
	public int Bombs;
	public int superBombs;
	public int metalScraps;
	public int gateKey1;
	public int gateKey2;

	private int r1;
	private int r2;
	private int r3;

	void Awake()
	{
		r1 = Random.Range (15, 45);
		r2 = Random.Range (15, 45);
		r3 = Random.Range (15, 45);

	}

	void Update(){
		transform.Rotate (new Vector3(r1,r2,r3) * Time.deltaTime);
	}

	 void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player"){
			Instantiate(pickUpSound, transform.position, transform.rotation);
			Destroy(transform.gameObject);
			//transform.gameObject.renderer.enabled = false;
			//audio.Play();
			//Destroy (transform.gameObject, audio.clip.length);
		}

	}
}
