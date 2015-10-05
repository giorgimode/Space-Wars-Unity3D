using UnityEngine;
using System.Collections;
using System.Linq;

public class NPCController : AdvancedFSM
{	//	public GameObject explosionSound;
    public GameObject explosion;
    public GameObject explosionAnimation;
    public GameObject deathSound;
    public GameObject deathAnimation;
    public GameObject dropOnDeath1;
    public GameObject dropOnDeath2;
    public GameObject dropOnDeath3;
    public GameObject dropKey;
    public int health;
    //	public float attackRange;
    public float speed;
    public float rotationSpeed;
    public int scoreValue;
    public float chaseStartDist;
    public float attackStartDist;
    public Vector3 patrolBounds;
    public float damageOnCrash;
    //	public Transform target;
    //private float distance;
    //	private RaycastHit hit;
    private bool chase = false;
    //private Done_GameController gameController;
    
   
    //  public GameObject Bullet;
    public bool kamikaze = false;
    //Initialize the Finite state machine for the NPC tank
    protected override void Initialize()
    {
        //health = 100;

        elapsedTime = 0.0f;
        shootRate = 2.0f;

        //Get the target (Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");

       
        playerTransform = objPlayer.transform;

        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

        //Get the turret of the tank
        //  turret = gameObject.transform.GetChild(0).transform;
        //   bulletSpawnPoint = turret.GetChild(0).transform;

        //Start Doing the Finite State Machine
        ConstructFSM();
    }

    protected override void AwakeFSM()
    {
       
        
    }

    //Update each frame
    protected override void UpdateFSM()
    {
        
        elapsedTime += Time.deltaTime;
    
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }

    protected override void FixedUpdateFSM()
    {
        CurrentState.Reason(playerTransform, transform);
        CurrentState.Act(playerTransform, transform);
    }

    public void SetTransition(FSMTransition t)
    {
        PerformTransition(t);
    }

    private void ConstructFSM()
    {
        //Get the list of points
        //   pointList = GameObject.FindGameObjectsWithTag("WandarPoint");
        var pointList = gameObject.transform.Cast<Transform>().Where(c => c.gameObject.tag == "WandarPoint").Select(c => c.gameObject).ToArray();

        Transform[] waypoints = new Transform[pointList.Length];
        int i = 0;


        foreach (GameObject obj in pointList)
        {
            obj.GetComponent<Transform>().position = transform.position + new Vector3(Random.onUnitSphere.x * 5,
                                                                 Random.onUnitSphere.y * 5, Random.onUnitSphere.z * 5);
            waypoints[i] = obj.transform;
            //	waypoints[i].position = new Vector3(Random.insideUnitSphere.x * 5, 
            //	                                    Random.insideUnitSphere.z * 5, Random.insideUnitSphere.z * 5);

            i++;
        }

        PatrolState patrol = new PatrolState(waypoints);
        patrol.AddTransition(FSMTransition.PlayerSeen, StateID.Chasing);
        patrol.AddTransition(FSMTransition.NoHealth, StateID.Dead);
        patrol.SetStateDistances(chaseStartDist, attackStartDist);
        patrol.SetSpeed(speed / 2, rotationSpeed / 2);
        patrol.SetBounds(patrolBounds.x, patrolBounds.y, patrolBounds.z);

        ChaseState chase = new ChaseState(waypoints);
        chase.AddTransition(FSMTransition.PlayerLost, StateID.Patrolling);
        chase.AddTransition(FSMTransition.PlayerReached, StateID.Attacking);
        chase.AddTransition(FSMTransition.NoHealth, StateID.Dead);
        chase.SetStateDistances(chaseStartDist, attackStartDist);
        chase.SetSpeed(speed, rotationSpeed);

        AttackState attack = new AttackState(waypoints);
        attack.AddTransition(FSMTransition.PlayerLost, StateID.Patrolling);
        attack.AddTransition(FSMTransition.PlayerSeen, StateID.Chasing);
        attack.AddTransition(FSMTransition.NoHealth, StateID.Dead);
        attack.SetStateDistances(chaseStartDist, attackStartDist);
        attack.SetSpeed(speed, rotationSpeed);
        attack.kamikaze = this.kamikaze;

        DeadState dead = new DeadState();
        dead.AddTransition(FSMTransition.NoHealth, StateID.Dead);

        AddFSMState(patrol);
        AddFSMState(chase);
        AddFSMState(attack);
        AddFSMState(dead);

        //Start patrolling at given points
        patrol.FindNextPoint();
    }

}
