using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour 
{
    //Player Transform
    protected Transform playerTransform;

    //Next destination position of the NPC 
    protected Vector3 destPos;

    //List of points for patrolling
    protected GameObject[] pointList;

    //Bullet shooting rate
    protected float shootRate;
    protected float elapsedTime;

    // Turret
    public Transform turret { get; set; }
    public Transform bulletSpawnPoint { get; set; }

    protected virtual void Initialize() { }
    protected virtual void UpdateFSM() { }
	protected virtual void AwakeFSM() { }
    protected virtual void FixedUpdateFSM() { }

	// Use this for initialization
	void Start () 
    {
        Initialize();
	}
	
	// Update is called once per frame
	void Update () 
    {
		UpdateFSM(); 

	}
	void Awake()
	{
		//	target = GameObject.FindGameObjectWithTag ("Player").transform;
		AwakeFSM();
	}

    void FixedUpdate()
    {
		FixedUpdateFSM();
    }    
}
