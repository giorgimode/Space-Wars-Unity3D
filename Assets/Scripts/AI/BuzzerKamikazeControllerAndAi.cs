using UnityEngine;
using System.Collections;

public class BuzzerKamikazeControllerAndAi : MonoBehaviour {

// Public member data
public MovementMotor motor;
public LineRenderer electricArc;
public AudioClip zapSound;
public float damageAmount = 5.0f;

private Transform player;
private Transform character;
private Vector3 spawnPos;
private float startTime;
private bool  threatRange = false;
private Vector3 direction;
private float rechargeTimer = 1.0f;
private AudioSource audioSource;
private Vector3 zapNoise = Vector3.zero;
//private GridPlayer pathPlayer;
void  Awake (){
	character = motor.transform;
	player = GameObject.FindWithTag ("Player").transform;
	
	spawnPos = character.position;
	audioSource = GetComponent<AudioSource>();
}

void  Start (){
	startTime = Time.time;
	motor.movementTarget = spawnPos;
	threatRange = false;
//	pathPlayer = GetComponent<GridPlayer>();
}

void Update()
{
  //  if (!pathPlayer.pathfindEnable)
   // {
    if (player!=null)
		motor.movementTarget = player.position;
		direction = (player.position - character.position);

		threatRange = false;
		if (direction.magnitude < 2.0f)
		{
			threatRange = true;
			motor.movementTarget = Vector3.zero;
		}

		rechargeTimer -= Time.deltaTime;

		if (rechargeTimer < 0.0f && threatRange && Vector3.Dot(character.forward, direction) > 0.8f)
		{
			zapNoise = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)) * 0.5f;
			Health targetHealth = player.GetComponent<Health>();
			if (targetHealth)
			{
				Vector3 playerDir = player.position - character.position;
				float playerDist = playerDir.magnitude;
				playerDir /= playerDist;
				targetHealth.OnDamage(damageAmount / (1.0f + zapNoise.magnitude), -playerDir);
			}

			DoElectricArc();

			rechargeTimer = Random.Range(1.0f, 2.0f);
		}
	}
//}

public void  DoElectricArc (){

	//if (electricArc.enabled)
	//     return;
	// Play attack sound
	audioSource.clip = zapSound;
	audioSource.Play ();
	
	//buzz.didChargeEfect = false;
	
	// Show electric arc
	electricArc.enabled = true;
	
	zapNoise = transform.rotation * zapNoise;
	
	// Ofset  electric arc texture while it's visible
	float stopTime = Time.time + 0.2f;
	while (Time.time < stopTime) {
		electricArc.SetPosition (0, electricArc.transform.position);
		electricArc.SetPosition (1, player.position + zapNoise);
		electricArc.sharedMaterial.mainTextureOffset = new Vector2(Random.value, 0.0f);  
		return;
	}
	
	// Hide electric arc
	electricArc.enabled = false;
}
}