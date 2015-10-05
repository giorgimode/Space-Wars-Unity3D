using UnityEngine;
using System.Collections;

public class Enemy_Turret : MonoBehaviour {

	public GameObject lasers;
	public Transform shotSpawn;

	public float aggroRange;	
	public float nextFire;
	public float fireRate;

	private Transform target;
	private float distance;
	private RaycastHit hit;

	void Awake()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{	if (target != null)		{
				distance = Vector3.Distance (transform.position, target.position);

				if (distance <= aggroRange) {

						if (Physics.Linecast (transform.position, target.position, out hit)) {

								if (hit.collider.tag == "Player" || hit.collider.tag == "Enemy" || hit.collider.tag == "PlayerProjectile" || hit.collider.tag == "PlayerSuperBomb" || hit.collider.tag == "PickUp") {

										transform.LookAt (target);

										if (Time.time > nextFire) {
												nextFire = Time.time + fireRate;
												Instantiate (lasers, shotSpawn.position, shotSpawn.rotation);

										}
								}
						}
				}
		}
	}
}
