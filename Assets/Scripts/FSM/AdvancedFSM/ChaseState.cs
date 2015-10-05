using UnityEngine;
using System.Collections;

public class ChaseState : FSMState
{	
    public ChaseState(Transform[] wp) 
    { 
        waypoints = wp;
        stateID = StateID.Chasing;

		//rotationSpeed = 20.0f;
		//Speed = 20.0f;
		//chaseStartDist = 40.0f;
		//attackStartDist = 20.0f;
        //find next Waypoint position
        FindNextPoint();
    }

    public override void Reason(Transform player, Transform npc)
    {
//        //Set the target position as the player position
//        destPos = player.position;

        //Check the distance with player 
        //When the distance is near, transition to attack state
        float dist = Vector3.Distance(npc.position, destPos);
		if (dist <= attackStartDist)
		{	
           // Debug.Log("Switch to Attack state");
            npc.GetComponent<NPCController>().SetTransition(FSMTransition.PlayerReached);
        }
        //Go back to patrol is it become too far
		else if (dist >= chaseStartDist)
		{	
          // Debug.Log("Switch to Patrol state");
            npc.GetComponent<NPCController>().SetTransition(FSMTransition.PlayerLost);
        }
    }

    public override void Act(Transform player, Transform npc)
	{ // Debug.Log ("chase act");
        //Rotate to the target point
        destPos = player.position;

        Quaternion targetRotation = Quaternion.LookRotation(destPos - npc.position);
        npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        //Go Forward
        npc.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
