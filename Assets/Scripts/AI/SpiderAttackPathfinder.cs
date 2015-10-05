using UnityEngine;
using System.Collections;

public class SpiderAttackPathfinder : Pathfinding
{

// Public member data
public MovementMotor motor;

public float targetDistanceMin = 2.0f;
public float targetDistanceMax = 3.0f;
public float proximityDistance = 4.0f;
public float damageRadius = 5.0f;
public float proximityBuildupTime = 2.0f;
public float proximityOfNoReturn = 0.6f;
public float damageAmount = 30.0f;
public Renderer proximityRenderer;
public AudioSource audioSource;
public SelfIlluminationBlink[] blinkComponents;
public GlowPlane blinkPlane;

public GameObject intentionalExplosion;
public MonoBehaviour animationBehaviour;

// Private memeber data
private AI ai;

private Transform character;

private Transform player;

private bool  inRange = false;
private float nextRaycastTime = 0;
private float lastRaycastSuccessfulTime = 0;
private float proximityLevel = 0;
private float lastBlinkTime = 0;
private float noticeTime = 0;
    //-----------------------------------------------
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
    if (blinkComponents.Length != 0)  // FIX HERE changed from 'if (!blinkComponents.Length)'
		blinkComponents = transform.parent.GetComponentsInChildren<SelfIlluminationBlink> ();

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
	nextRaycastTime = Time.time;
	lastRaycastSuccessfulTime = Time.time;
	noticeTime = Time.time;
	animationBehaviour.enabled = true;
	if (blinkPlane) 
		blinkPlane.GetComponent<Renderer>().enabled = false;	
}

void  OnDisable (){
	if (proximityRenderer == null)
		Debug.LogError ("proximityRenderer is null", this);
	else if (proximityRenderer.material == null)
		Debug.LogError ("proximityRenderer.material is null", this);
	else
		proximityRenderer.material.color = Color.white;
	if (blinkPlane) 
		blinkPlane.GetComponent<Renderer>().enabled = false;
}

void  Update (){
    FindPath();
    if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition)
    {
        Mover();

    }

    //if (Path.Count == 3)
    //{
    ////    movementMotor.enabled = true;
    //    // AIcontroller.enabled = true;
    //    pathfindEnable = false;
    //    //enabled = false;
    //}
}

private void Mover()
{
    if (Path.Count > 0 && player != null)
    {
        if (Time.time < noticeTime + 0.7f)
        {
            motor.movementDirection = Vector3.zero;
            return;
        }
        Vector3 direction = (Path[0] - character.position).normalized;

        // Calculate the direction from the player to this character
        Vector3 playerDirection = (player.position - character.position);
        playerDirection.y = 0;
        float playerDist = playerDirection.magnitude;
            //Vector3.Distance(transform.position, testPlayer.transform.position);
        playerDirection /= playerDist;

      //  Vector3 playerDirection = (Path[0] - character.position).normalized;
        // Set this character to face the player,
        // that is, to face the direction from this character to the player
        //motor.facingDirection = playerDirection;

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
           

        //if (transform.position.x < Path[0].x + 0.4F && transform.position.x > Path[0].x - 0.4F && transform.position.z > Path[0].z - 0.4F && transform.position.z < Path[0].z + 0.4F)
        //{
        //    Path.RemoveAt(0);

        //}
        //RaycastHit[] hit = Physics.RaycastAll(transform.position + (Vector3.up * 20F), Vector3.down, 100);
        //float maxY = -Mathf.Infinity;
        //foreach (RaycastHit h in hit)
        //{
        //    if (h.transform.tag == "Untagged")
        //    {
        //        if (maxY < h.point.y)
        //        {
        //            maxY = h.point.y;
        //        }
        //    }
        //}
        //transform.position = new Vector3(transform.position.x, maxY + 1F, transform.position.z);

        if ((playerDist < proximityDistance && Time.time < lastRaycastSuccessfulTime + 1) || proximityLevel > proximityOfNoReturn)
            proximityLevel += Time.deltaTime / proximityBuildupTime;
        else
            proximityLevel -= Time.deltaTime / proximityBuildupTime;

        proximityLevel = Mathf.Clamp01(proximityLevel);
        //proximityRenderer.material.color = Color.Lerp (Color.blue, Color.red, proximityLevel);
        if (proximityLevel == 1)
            Explode();

        if (Time.time > nextRaycastTime)
        {
            nextRaycastTime = Time.time + 1;
            if (ai.CanSeePlayer())
            {
                lastRaycastSuccessfulTime = Time.time;
            }
            else
            {
                if (Time.time > lastRaycastSuccessfulTime + 2)
                {
                    ai.OnLostTrack();
                }
            }
        }

        float deltaBlink = 1 / Mathf.Lerp(2, 15, proximityLevel);
        if (Time.time > lastBlinkTime + deltaBlink)
        {
            lastBlinkTime = Time.time;
            proximityRenderer.material.color = Color.red;
            if (audioSource.enabled)
            {
                audioSource.Play();
            }
            foreach (SelfIlluminationBlink comp in blinkComponents)
            {
                comp.Blink();
            }
            if (blinkPlane)
                blinkPlane.GetComponent<Renderer>().enabled = !blinkPlane.GetComponent<Renderer>().enabled;
        }
        if (Time.time > lastBlinkTime + 0.04f)
        {
            proximityRenderer.material.color = Color.white;
        }
    }
}

void  Explode (){
	float damageFraction = 1 - (Vector3.Distance (player.position, character.position) / damageRadius);
	
	Health targetHealth = player.GetComponent<Health> ();
	if (targetHealth) {
		// Apply damage
		targetHealth.OnDamage (damageAmount * damageFraction, character.position - player.position);
	}
	player.GetComponent<Rigidbody>().AddExplosionForce (10, character.position, damageRadius, 0.0f, ForceMode.Impulse);
	Spawner.Spawn (intentionalExplosion, transform.position, Quaternion.identity);
	Spawner.Destroy (character.gameObject);
}

private void FindPath()
{

   // if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition)
   // {
    if (testPlayerTransform != null)
        FindPath(transform.position, testPlayerTransform.position);

   // }
}

}
