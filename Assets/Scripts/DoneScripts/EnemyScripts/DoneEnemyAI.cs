using UnityEngine;
using System.Collections;

public class DoneEnemyAI : MonoBehaviour
{
	public float patrolSpeed = 2f;							// The nav mesh agent's speed when patrolling.
	public float chaseSpeed = 5f;							// The nav mesh agent's speed when chasing.
	public float chaseWaitTime = 5f;						// The amount of time to wait when the last sighting is reached.
	public float patrolWaitTime = 1f;						// The amount of time to wait when the patrol way point is reached.
	public Transform[] patrolWayPoints;						// An array of transforms for the patrol route.
    public float health=50;
    public GameObject bloodPrefab;
    public GameObject enemyDeathSound;
	private DoneEnemySight enemySight;						// Reference to the EnemySight script.
	private NavMeshAgent nav;								// Reference to the nav mesh agent.
	private Transform player;								// Reference to the player's transform.
	private Health playerHealth;					// Reference to the PlayerHealth script.
	private DoneLastPlayerSighting lastPlayerSighting;		// Reference to the last global sighting of the player.
	private float chaseTimer;								// A timer for the chaseWaitTime.
	private float patrolTimer;								// A timer for the patrolWaitTime.
	private int wayPointIndex;								// A counter for the way point array.
	private Transform damageEffectTransform;
    private ParticleEmitter damageEffect;
    private float colliderRadiusHeuristic = 1.0f;
    private float damageEffectCenterYOffset;
	void Awake ()
	{
		// Setting up the references.
		enemySight = GetComponent<DoneEnemySight>();
		nav = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag(DoneTags.player).transform;
		playerHealth = player.GetComponent<Health>();
		lastPlayerSighting = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<DoneLastPlayerSighting>();

        if (bloodPrefab)
        {
           // if (damageEffectTransform == gameObject.transform)
                damageEffectTransform = transform;
            GameObject effect = Spawner.Spawn(bloodPrefab, Vector3.zero, Quaternion.identity);
            effect.transform.parent = damageEffectTransform;
            effect.transform.localPosition = Vector3.zero;
            damageEffect = effect.GetComponent<ParticleEmitter>();
            Vector2 tempSize = new Vector2(GetComponent<Collider>().bounds.extents.x, GetComponent<Collider>().bounds.extents.z);
            colliderRadiusHeuristic = tempSize.magnitude * 0.5f;
            damageEffectCenterYOffset = GetComponent<Collider>().bounds.extents.y;

        }
    }
	
	
	void Update ()
	{
		// If the player is in sight and is alive...
        if (enemySight.playerInSight && playerHealth.health > 0f)
        // ... shoot.
        Shooting(); 

        // If the player has been sighted and isn't dead...
        else if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition && playerHealth.health > 0f && !enemySight.playerInSight)
        // ... chase.
        Chasing(); 

        // Otherwise...
        else
        // ... patrol.
        Patrolling(); 
	}

    public void takeDamage(float damage, Vector3 fromDirection)
    {
        health -= damage;
        if (damageEffect)
        {
            damageEffect.transform.rotation = Quaternion.LookRotation(fromDirection, Vector3.up);
          //  if (!damageEffectCentered)
           // {
                Vector3 dir = fromDirection;
                dir.y = 0.0f;
                damageEffect.transform.position = (transform.position + Vector3.up * damageEffectCenterYOffset) + colliderRadiusHeuristic * dir;
         //   }

            damageEffect.Emit();// (particleAmount);
        }
        if (health <= 0)
        {
            Instantiate(enemyDeathSound, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
	
	void Shooting ()
	{
		// Stop the enemy where it is.
		nav.Stop();
	}
	
	
	void Chasing ()
    {
        nav.Resume();
		// Create a vector from the enemy to the last sighting of the player.
		Vector3 sightingDeltaPos = player.position - transform.position;
		
		// If the the last personal sighting of the player is not close...
		if(sightingDeltaPos.sqrMagnitude > 4f)
			// ... set the destination for the NavMeshAgent to the last personal sighting of the player.
			nav.destination = player.position;
		
		// Set the appropriate speed for the NavMeshAgent.
		nav.speed = chaseSpeed;
		
		// If near the last personal sighting...
		//if(nav.remainingDistance < nav.stoppingDistance)
		//{
			// ... increment the timer.
			chaseTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(chaseTimer >= chaseWaitTime)
			{
				// ... reset last global sighting, the last personal sighting and the timer.
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
				chaseTimer = 0f;
			}
		//}
	//	else
			// If not near the last sighting personal sighting of the player, reset the timer.
		//	chaseTimer = 0f;
	}

	
	void Patrolling ()
	{
		// Set an appropriate speed for the NavMeshAgent.
		nav.speed = patrolSpeed;
		
		// If near the next waypoint or there is no destination...
		if(nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
		{
			// ... increment the timer.
			patrolTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(patrolTimer >= patrolWaitTime)
			{
				// ... increment the wayPointIndex.
				if(wayPointIndex == patrolWayPoints.Length - 1)
					wayPointIndex = 0;
				else
					wayPointIndex++;
				
				// Reset the timer.
				patrolTimer = 0;
			}
		}
		else
			// If not near a destination, reset the timer.
			patrolTimer = 0;
		
		// Set the destination to the patrolWayPoint.
		nav.destination = patrolWayPoints[wayPointIndex].position;
	}
}
