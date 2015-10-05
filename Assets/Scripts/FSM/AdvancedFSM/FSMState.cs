using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class FSMState
{
    protected Dictionary<FSMTransition, StateID> map = new Dictionary<FSMTransition, StateID>();
    protected StateID stateID;
    public StateID ID { get { return stateID; } }
    protected Vector3 destPos;
	protected Transform[] waypoints;
    protected float rotationSpeed;
    protected float speed;
	protected float chaseStartDist;
	protected float attackStartDist;
	protected Vector3 patrolBounds;

	public void SetStateDistances(float chase, float attack)
	{
		chaseStartDist = chase;
		attackStartDist = attack;

	}

	public void SetSpeed(float sp, float rotSpeed)
	{
		speed = sp;
		rotationSpeed = rotSpeed;
		
	}

	public void SetBounds(float x, float y, float z)
	{
		patrolBounds.x = x;
		patrolBounds.y = y;
		patrolBounds.z = z;
		
	}

    public void AddTransition(FSMTransition transition, StateID id)
    {
        // Check if anyone of the args is invallid
        if (transition == FSMTransition.None || id == StateID.None)
        {
            Debug.LogWarning("FSMState : Null transition not allowed");
            return;
        }

        //Since this is a Deterministc FSM,
        //Check if the current transition was already inside the map
        if (map.ContainsKey(transition))
        {
            Debug.LogWarning("FSMState ERROR: transition is already inside the map");
            return;
        }

        map.Add(transition, id);
      //  Debug.Log("Added : " + transition + " with ID : " + id);
    }


    /// This method deletes a pair transition-state from this state´s map.
    /// If the transition was not inside the state´s map, an ERROR message is printed.
    public void DeleteTransition(FSMTransition trans)
    {
        // Check for NullTransition
        if (trans == FSMTransition.None)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return;
        }

        // Check if the pair is inside the map before deleting
        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }
        Debug.LogError("FSMState ERROR: FSMTransition passed was not on this State´s List");
    }



    /// This method returns the new state the FSM should be if
    ///    this state receives a transition  
    public StateID GetOutputState(FSMTransition trans)
    {
        // Check for NullTransition
        if (trans == FSMTransition.None)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return StateID.None;
        }

        // Check if the map has this transition
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }

        Debug.LogError("FSMState ERROR: " + trans+ " FSMTransition passed to the State was not on the list");
        return StateID.None;
    }


    /// Decides if the state should transition to another on its list
    /// NPC is a reference to the npc that is controlled by this class
    public abstract void Reason(Transform player, Transform npc);

    /// 
    /// This method controls the behavior of the NPC in the game World.
    /// Every action, movement or communication the NPC does should be placed here
    /// NPC is a reference to the npc tha is controlled by this class
    public abstract void Act(Transform player, Transform npc);


    /// Find the next semi-random patrol point
    public void FindNextPoint()
    {
        //Debug.Log("Finding next point");
        int rndIndex = Random.Range(0, waypoints.Length);
		Vector3 rndPosition = new Vector3(Random.onUnitSphere.x * patrolBounds.x, 
		                                  Random.onUnitSphere.y * patrolBounds.y, Random.onUnitSphere.z *  patrolBounds.z);
//		Debug.Log ("random");
        destPos = waypoints[rndIndex].position + rndPosition;
	  }

    /// Check whether the next random position is the same as current NPC position
    /// <param name="pos">position to check</param>
    protected bool IsInCurrentRange(Transform trans, Vector3 pos)
    {
        float xPos = Mathf.Abs(pos.x - trans.position.x);
        float zPos = Mathf.Abs(pos.z - trans.position.z);
		float yPos = Mathf.Abs(pos.y - trans.position.y);

		if (xPos <= 5 && zPos <= 5 && yPos <= 5)
            return true;

        return false;
    }
}
