using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flock : MonoBehaviour
{
    internal FlockController controller;
    private float distance;
    private NPCController npc;
    private Done_WeaponController weapon;
    private FlockController flockControl;
    private Flock flock;
    private GameObject objPlayer;

    void Start()
    {
        objPlayer = GameObject.FindGameObjectWithTag("Player");
        // enable/disable NPCController script if attacking
        npc = gameObject.GetComponent<NPCController>();
        weapon = gameObject.GetComponent<Done_WeaponController>();
        flockControl = gameObject.transform.parent.GetComponent<FlockController>();
        flock = gameObject.transform.parent.GetComponent<Flock>();
        //	Debug.Log(npc.GetPlayerTransform().position + "player place");
    }

    void Update()
    {
        if (controller && gameObject != null)
        {
            //check if target is destroyed

            distance = Vector3.Distance(objPlayer.transform.position, transform.position);
            
            if (distance <= npc.chaseStartDist || controller.leader == null)
            {   
                npc.enabled = true;
                weapon.enabled = true;
             //   flockControl.enabled = false;
//                flock.enabled = false;
             //   Debug.Log("flock is off");
             // If player attacks, enemy is not controlled by flock anymore
                flockControl.flockList.Remove(this);

            }
            else if (distance > npc.chaseStartDist && controller.leader != null)
            {
                
                npc.enabled = false;
                weapon.enabled = false;
             //   flockControl.enabled = true;
                // add non-player back to flock once enemy is gone
               if (!flockControl.flockList.Contains(this))
                   flockControl.flockList.Add(this);
              
                
                Vector3 relativePos = steer() * Time.deltaTime;

                if (relativePos != Vector3.zero)
                    GetComponent<Rigidbody>().velocity = relativePos;

                // enforce minimum and maximum speeds for the boids
                float speed = GetComponent<Rigidbody>().velocity.magnitude;
                if (speed > controller.maxVelocity)
                {
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * controller.maxVelocity;
                }
                else if (speed < controller.minVelocity)
                {
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * controller.minVelocity;
                }
            }

        }
    }

    //Calculate flock steering Vector based on the Craig Reynold's algorithm (Cohesion, Alignment, Follow leader and Seperation)
    private Vector3 steer()
    {
        Vector3 center = controller.flockCenter - transform.localPosition;			// cohesion
        Vector3 velocity = controller.flockVelocity - GetComponent<Rigidbody>().velocity; 			// alignment
        Vector3 follow = controller.leader.localPosition - transform.localPosition; // follow leader
        Vector3 separation = Vector3.zero; 											// separation

        foreach (Flock flock in controller.flockList)
        {
            if (flock != this && flock != null)
            {
                Vector3 relativePos = transform.localPosition - flock.transform.localPosition;
                separation += relativePos / (relativePos.sqrMagnitude);
            }
        }

        // randomize
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);

        randomize.Normalize();

        return (controller.centerWeight * center +
            controller.velocityWeight * velocity +
            controller.separationWeight * separation +
            controller.followWeight * follow +
            controller.randomizeWeight * randomize);
    }

    //void OnCollisionEnter(Collision other)
    //{
        
    //        if (other.gameObject.tag == "Enemy")
    //        {
    //        }
    //        else if (other.gameObject.tag == "EnemyProjectile")
    //        {
    //        }
    //        else if (other.gameObject.tag == "PickUp")
    //        {
    //        }
    //        else if (other.gameObject.tag == "Boss3")
    //        {
    //        }
    //        else if (other.gameObject.tag == "CheckPoint")
    //        {
    //        }
    //        else if (other.gameObject.tag == "FinalBattle")
    //        {
    //        }
    //        else if (other.gameObject.tag == "WinPoint")
    //        {
    //        }

    //        else if (other.gameObject.tag == "PlayerProjectile")
    //        {
    //            npc.enabled = true;
    //            weapon.enabled = true;
    //            flockControl.enabled = false;

    //        }

    //        else Debug.Log("Collision with unknown object while flocking");
       
    //}
}