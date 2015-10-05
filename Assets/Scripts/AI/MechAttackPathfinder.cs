﻿using UnityEngine;
using System.Collections;

public class MechAttackPathfinder : Pathfinding
{

// Public member data
public MovementMotor motor;
public Transform head;

public float targetDistanceMin = 3.0f;
public float targetDistanceMax = 4.0f;

public MonoBehaviour[] weaponBehaviours;
public float fireFrequency = 2;

// Private memeber data
private AI ai;
     
private Transform character;

private Transform player;

private bool  inRange = false;
private float nextRaycastTime = 0;
private float lastRaycastSuccessfulTime = 0;
private float noticeTime = 0;

private bool  firing = false;
private float lastFireTime = -1;
private int nextWeaponToFire = 0;
    //----------------------
private GameObject testPlayer;
private Transform testPlayerTransform;
private KamikazeMovementMotor movementMotor;
private BuzzerKamikazeControllerAndAi AIcontroller;
float distance;
private DoneLastPlayerSighting lastPlayerSighting;
private EnemySight enemySight;
[HideInInspector]
public bool pathfindEnable = false;


void  Awake (){
	character = motor.transform;
	player = GameObject.FindWithTag ("Player").transform;
	ai = transform.parent.GetComponentInChildren<AI> ();
    
    lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<DoneLastPlayerSighting>();
    enemySight = GetComponent<EnemySight>();
}

void Start()
{

    testPlayer = GameObject.FindWithTag("Player");
    testPlayerTransform = testPlayer.transform;
}

void  OnEnable (){
	inRange = false;
	nextRaycastTime = Time.time + 1;
	lastRaycastSuccessfulTime = Time.time;
	noticeTime = Time.time;
}

void  OnDisable (){
	Shoot (false);
}

void  Shoot ( bool state  ){
	 firing = state;
}

void  Fire (){
	if (weaponBehaviours[nextWeaponToFire]) {
		weaponBehaviours[nextWeaponToFire].SendMessage ("Fire");
		nextWeaponToFire = (nextWeaponToFire + 1) % weaponBehaviours.Length;
		lastFireTime = Time.time;
	}
}

void  Update (){
    FindPath();
    if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition)
    {
        Attack();

    }
   
}

bool IsAimingAtPlayer (){
	 Vector3 playerDirection = (player.position - head.position);
	playerDirection.y = 0;
	return Vector3.Angle (head.forward, playerDirection) < 15;
}

void Attack()
{
    if (Path.Count > 0 && player!=null)
    {
        // Calculate the direction from the player to this character
        Vector3 direction = (Path[0] - character.position).normalized;

        Vector3 playerDirection = (player.position - character.position);
        playerDirection.y = 0;
        float playerDist = playerDirection.magnitude;
        playerDirection /= playerDist;

        // Set this character to face the player,
        // that is, to face the direction from this character to the player
        motor.facingDirection = playerDirection;

        // For a short moment after noticing player,
        // only look at him but don't walk towards or attack yet.
        if (Time.time < noticeTime + 1.5f)
        {
            motor.movementDirection = Vector3.zero;
            return;
        }

        if (inRange && playerDist > targetDistanceMax)
            inRange = false;
        if (!inRange && playerDist < targetDistanceMin)
            inRange = true;

        if (inRange)
            motor.movementDirection = Vector3.zero;
        else
        {
            // direction = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * 14F);
            motor.movementDirection = direction;
            //if close to the waypoint on path, remove waypoint
            if (character.position.x < Path[0].x + 0.4F && character.position.x > Path[0].x - 0.4F && character.position.z > Path[0].z - 0.4F && character.position.z < Path[0].z + 0.4F)
            {
                Path.RemoveAt(0);

            }
        }

        if (Time.time > nextRaycastTime)
        {
            nextRaycastTime = Time.time + 1;
            if (ai.CanSeePlayer())
            {
                lastRaycastSuccessfulTime = Time.time;
                if (IsAimingAtPlayer())
                    Shoot(true);
                else
                    Shoot(false);
            }
            else
            {
                Shoot(false);
                if (Time.time > lastRaycastSuccessfulTime + 5)
                {
                    ai.OnLostTrack();
                }
            }
        }

        if (firing)
        {
            if (Time.time > lastFireTime + 1 / fireFrequency)
            {
                Fire();
            }
        }
    }
}

private void FindPath()
{

    // if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition)
    // {
    if (testPlayerTransform!=null)
    FindPath(transform.position, testPlayerTransform.position);

    // }
}
}

