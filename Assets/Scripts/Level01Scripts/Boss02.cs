using UnityEngine;
using System.Collections;

public class Boss02 : MonoBehaviour {

static T GetRandomEnum<T>()
{
	System.Array A = System.Enum.GetValues(typeof(T));
	T V = (T)A.GetValue(UnityEngine.Random.Range(0,A.Length));
	return V;
}

private GameObject Player;
//private GameObject Level;
private GameObject[] Shots;
public float health = 250.0f;
private float nextBoltFire = 0.0f;
private float nextRocketFire = 0.0f;
private float nextSeekerFire = 0.0f;
public float fireRate = 0.25f;
public GameObject laserBeam;
public GameObject rocket;
public GameObject seeker;


public enum bossState{idel, activating, seeking, shielded, shooting, wave, dead};
private enum shotMode{laserFire, rocketFire, seekerFire};
private shotMode shotState;
public bossState myBossState;
private int RangeMax;
private GameObject shield;
private float seekRange = 500.0f;
private float height = 1000.0f;
private float speed = 0.0f;
private float duration;
private float shieldTime;
private float shieldDuration = 5.0f;
public float speedMulti = 1.5f;
	private float vel = 0.0f;
	private const float acc = 500.0f;

	// Use this for initialization
void Start () {
	
	Player = GameObject.FindGameObjectWithTag ("Player");
	//Level = GameObject.Find ("Level");
	myBossState = bossState.idel;
	Shots = GameObject.FindGameObjectsWithTag ("hardpoint");
	RangeMax = Shots.Length;
	shield = GameObject.Find ("Shield");
	speed = Player.GetComponent<PlayerMovement> ().speed * speedMulti;
}

// Update is called once per frame
void Update () 
{
	switch (myBossState) 
	{
	case bossState.idel:
		break;
	case bossState.activating:
		activateing();
		break;
	case bossState.seeking:
		seeking();
		break;
	case bossState.shielded:
		shielded();
		break;
	case bossState.shooting:
		ShootPlayer();
		break;
	case bossState.wave:
		wave();
		break;
	case bossState.dead:
		dead ();
		break;
	default:
		break;
	}
}

void OnTriggerEnter(Collider other)
{
	
	if ((other.tag == "PlayerProjectile") ||(other.tag == "PlayerSuperBomb") )
	{
		
		PlayerProjectile playerProjectile = other.GetComponent<PlayerProjectile>();
		takeDamage(playerProjectile.damage);
		if(other.tag == "PlayerProjectile") 
		{
			Destroy (other.gameObject, 1.0f);
		}
	}
}

private void takeDamage(float damage)
{
	health -= damage;
	if (health <= 0) { myBossState = bossState.dead;}
}

private void randomFire()
{
	if (Time.time > nextBoltFire) {
		
		nextBoltFire = Time.time + fireRate;
		int bolts = Random.Range (10, RangeMax);
		
		for (int i = 0; i < bolts; i++) {
			GameObject go = Shots [Random.Range (0, RangeMax)];
			go.transform.LookAt (Player.transform);
			Instantiate (laserBeam, go.transform.position, go.transform.rotation);
		}
	}
}

private void randomRocket()
{
	if (Time.time > nextRocketFire) {
		
		nextRocketFire = Time.time + (fireRate * 10.0f);
		int bolts = Random.Range (10, RangeMax);
		
		for (int i = 0; i < bolts; i++) {
			GameObject go = Shots [Random.Range (0, RangeMax)];
			go.transform.LookAt (Player.transform);
			Instantiate (seeker, go.transform.position, go.transform.rotation);
		}
	}
}

private void randomSeeker()
{
	if (Time.time > nextSeekerFire) {
		
		nextSeekerFire = Time.time + (fireRate * 2.5f);
		int bolts = Random.Range (10, RangeMax);
		
		for (int i = 0; i < bolts; i++) {
			GameObject go = Shots [Random.Range (0, RangeMax)];
			go.transform.LookAt (Player.transform);
			Instantiate (rocket, go.transform.position, go.transform.rotation);
		}
	}
}

private void FireAll()
{
	if (Time.time > nextBoltFire) {
		
		nextBoltFire = Time.time + fireRate;
		foreach (GameObject go in Shots) {
			go.transform.LookAt (Player.transform);
			Instantiate (laserBeam, go.transform.position, go.transform.rotation);
		}
	}
}

private void FireAllRocket()
{
	if (Time.time > nextRocketFire) {
		
		nextRocketFire = Time.time + (fireRate * 2.5f);
		foreach (GameObject go in Shots) {
			go.transform.LookAt (Player.transform);
			Instantiate (rocket, go.transform.position, go.transform.rotation);
		}
	}
}

private void FireAllSeeker()
{
	if (Time.time > nextSeekerFire) {
		
		nextSeekerFire = Time.time + (fireRate * 10.0f);
		foreach (GameObject go in Shots) {
			go.transform.LookAt (Player.transform);
			Instantiate (seeker, go.transform.position, go.transform.rotation);
		}
	}
}

public void Activate()
{
	myBossState = bossState.activating;
}

private void activateing()
{
	Debug.Log ("height = " + gameObject.transform.position.y.ToString ());
	if (gameObject.transform.position.y >= height) {
		
		if(vel > 0)
		{	
			vel = vel - acc * Time.deltaTime;
			Vector3 pos = gameObject.transform.position;
			pos.y = gameObject.transform.position.y + vel * Time.deltaTime;
			gameObject.transform.position = pos;
		}
		else
		{
			myBossState = bossState.shielded;
		}
		
	} 
	else
	{
			setShield(true);

		if(vel < speed)
		{	
			vel = vel + (acc * Time.deltaTime);
		}
		Vector3 pos = gameObject.transform.position;
		pos.y = gameObject.transform.position.y + vel * Time.deltaTime;
		gameObject.transform.position = pos;
	}
}


private void seeking()
{
	if (Vector3.Distance (Player.transform.position, gameObject.transform.position) > seekRange)
	{
		Vector3 direction = (Player.transform.position - gameObject.transform.position);
		direction.Normalize ();
		gameObject.transform.position = gameObject.transform.position  + (direction * speed * Time.deltaTime);
	}
	else
	{
		duration = Time.time + Random.Range(7.0f, 15.0f);
		shotState  = GetRandomEnum<shotMode>();
		myBossState = bossState.shooting;
	}
}

private void shielded()
{
	setShield (true);
	if(Time.time > shieldTime)
	{
		setShield(false);
		myBossState = bossState.seeking;
	}
}



private void ShootPlayer()
{
	
	if(Time.time > duration)
	{
		myBossState  = bossState.wave;
	}
	else
	{
		switch(shotState)
		{
		case shotMode.laserFire:
			randomFire();
			break;
//		case shotMode.maxLaserFire:
//			FireAll();
//			break;
		case shotMode.rocketFire:
			randomRocket();
			break;
//		case shotMode.maxRocketFire:
//			FireAllRocket();
//			break;
		case shotMode.seekerFire:
			randomSeeker();
			break;
//		case shotMode.maxSeekerFire:
//			FireAllSeeker();
//			break;
//		case shotMode.WW3:
//			FireAllRocket();
//			FireAllSeeker();
//			FireAll();
//			randomRocket();
//			randomSeeker();
//			randomFire();
//			break;
			
		}
	}
}

private void wave()
{
	shieldTime = Time.time + shieldDuration;
	myBossState = bossState.shielded;
}

private void dead()
{
	Destroy (gameObject);
}

private void setShield(bool turnOn)
{
	shield.GetComponent<MeshRenderer> ().enabled = turnOn;
	shield.GetComponent<SphereCollider> ().enabled = turnOn;
	shield.GetComponent<ShieldSpin> ().enabled = turnOn;
}

}

