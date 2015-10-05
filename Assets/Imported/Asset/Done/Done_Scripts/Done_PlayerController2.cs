using UnityEngine;
using System.Collections;

public class Done_PlayerController2 : MonoBehaviour {
	
	//place weapon prefabs into the script
	public GameObject lasers;
	public GameObject flameThrower;
	public GameObject lightningGun;
	public GameObject missiles;
	public GameObject bombs;
	public float health = 40;
	public Transform shotSpawn; //location where projectile spawns relative to the player controller
	
	public float nextFire;
	public float fireRate;
	
	//set each weapon to true as it is unlocked
	private bool availableFlameThrower = false;
	private bool availableLightningGun = false;
	private bool availableBombs = false;
	
	private int primaryWeaponSelected = 1; //laser-1, flame thrower-2, lightning gun-3
	private int secondaryWeaponSelected = 4; //missiles-4, bombs-5
	
	//ammo count of each projectile
	private int ammoFlameThrower = 0;
	private int ammoLightningGun = 0;
	private int ammoMissiles = 10;
	private int ammoBombs = 0;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.Alpha1))
			primaryWeaponSelected = 1;
		if (Input.GetKeyDown (KeyCode.Alpha2) && availableFlameThrower)
			primaryWeaponSelected = 2;
		if (Input.GetKeyDown (KeyCode.Alpha3) && availableLightningGun)
			primaryWeaponSelected = 3;
		
		if (Input.GetKeyDown (KeyCode.Alpha4))
			secondaryWeaponSelected = 4;
		if (Input.GetKeyDown (KeyCode.Alpha5) && availableBombs)
			secondaryWeaponSelected = 5;
		if (Input.GetKeyDown (KeyCode.Alpha6))
			secondaryWeaponSelected = 6;
		
		switch (primaryWeaponSelected)
		{
		case 1:
			if (Input.GetButton ("Fire1") && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Instantiate (lasers, shotSpawn.position, shotSpawn.rotation);
			}
			break;
		case 2:
			if (Input.GetButton ("Fire1") && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Instantiate (flameThrower, shotSpawn.position, shotSpawn.rotation);
			}
			break;
		case 3:
			if (Input.GetButton ("Fire1") && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Instantiate (lightningGun, shotSpawn.position, shotSpawn.rotation);
			}
			break;
		}
		
		switch(secondaryWeaponSelected)
		{
		case 4:
			if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > nextFire && ammoMissiles > 0)
			{
				nextFire = Time.time + fireRate;
				Instantiate (missiles, shotSpawn.position, shotSpawn.rotation);
				ammoMissiles--;
			}
			break;
		case 5:
			if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				Instantiate (lightningGun, shotSpawn.position, shotSpawn.rotation);
			}
			break;
		case 6:
			if (Input.GetButton ("Fire1") && Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				Instantiate (lightningGun, shotSpawn.position, shotSpawn.rotation);
			}
			break;
		}
		
		
	}
}
