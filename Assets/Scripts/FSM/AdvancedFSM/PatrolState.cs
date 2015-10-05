using UnityEngine;
using System.Collections;

public class PatrolState : FSMState
{
    public PatrolState(Transform[] wp) 
    { 
        waypoints = wp; 
        stateID = StateID.Patrolling;

//        rotationSpeed = rotationSpeed/2;
//		speed = speed / 2;
    //    Speed = 20.0f;
		//chaseStartDist = 40.0f;
		//attackStartDist = 20.0f;
    }

    public override void Reason(Transform player, Transform npc)
    {
        //Check the distance with player 
        //When the distance is near, transition to chase state
		if (player!=null && Vector3.Distance(npc.position, player.position) <= chaseStartDist && player!=null)
		{	 
            npc.GetComponent<NPCController>().SetTransition(FSMTransition.PlayerSeen);
        }
    }

    public override void Act(Transform player, Transform npc)
    {	
        //Find another random patrol point if the current point is reached
//        Debug.Log("flock test: " + destPos);
        if (Vector3.Distance(npc.position, destPos) <= 2.0f)
		{ 
            FindNextPoint();
        }
		//FindNextPoint ();
//		Debug.Log ("patrol act 2");
	//	Debug.Log (destPos + "--" + npc.position);
        //Rotate to the target point
		Quaternion targetRotation = Quaternion.LookRotation(destPos - npc.position); 
        npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        //Go Forward
        npc.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}