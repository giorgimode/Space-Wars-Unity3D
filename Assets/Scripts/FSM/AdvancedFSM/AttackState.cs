using UnityEngine;
using System.Collections;

public class AttackState : FSMState
{		public bool kamikaze;
	//public float attackStartDist;
//	public float attackStartDist;

    public AttackState(Transform[] wp) 
    { 
        waypoints = wp;
        stateID = StateID.Attacking;
	//	rotationSpeed = 20.0f;
	//	Speed = 20.0f;
		//chaseStartDist = 40.0f;
		//attackStartDist = 20.0f;

        //find next Waypoint position
        FindNextPoint();
    }

    public override void Reason(Transform player, Transform npc)
    {
        //Check the distance with the player tank
        float dist = Vector3.Distance(npc.position, player.position);
		if (dist >= attackStartDist && dist < chaseStartDist)
		{  // Debug.Log ("rotate to the target point");
            //Rotate to the target point
//            Quaternion targetRotation = Quaternion.LookRotation(destPos - npc.position);
//            npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * curRotSpeed);
//
//            //Go Forward
//            npc.Translate(Vector3.forward * Time.deltaTime * curSpeed);

        //    Debug.Log("Switch to Chase State");
            npc.GetComponent<NPCController>().SetTransition(FSMTransition.PlayerSeen);
        }
        //FSMTransition to patrol is the tank become too far
		else if (dist >= chaseStartDist)
		{	
            Debug.Log("Switch to Patrol State");
            npc.GetComponent<NPCController>().SetTransition(FSMTransition.PlayerLost);
        }  
    }

    public override void Act(Transform player, Transform npc)
	{//	Debug.Log ("attack act");
        //Set the target position as the player position
        destPos = player.position;
		Quaternion targetRotation = Quaternion.LookRotation (destPos - npc.position);
		npc.rotation = Quaternion.Slerp (npc.rotation, targetRotation, Time.deltaTime * rotationSpeed);

	  //Use for kamikaze	
		if (kamikaze) {
			targetRotation = Quaternion.LookRotation (destPos - npc.position);
			npc.rotation = Quaternion.Slerp (npc.rotation, targetRotation, Time.deltaTime * rotationSpeed);
		
			//Go Forward
			npc.Translate (Vector3.forward * Time.deltaTime * speed);
//			if (Vector3.Distance(destPos,npc.position) <10) {  
//				Debug.Log("Shoot Shoot");
//			}

		}

        //Always Turn the turret towards the player
        // Transform turret = npc.GetComponent<NPCTankController>().turret;
        // Quaternion turretRotation = Quaternion.LookRotation(destPos - turret.position);
        // turret.rotation = Quaternion.Slerp(turret.rotation, turretRotation, Time.deltaTime * curRotSpeed *10);

        //Shoot bullet towards the player
      //  npc.GetComponent<NPCTankController>().ShootBullet();
    }
}
