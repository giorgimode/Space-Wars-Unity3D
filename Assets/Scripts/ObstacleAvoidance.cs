using UnityEngine;
using System.Collections;

public class ObstacleAvoidance : MonoBehaviour
{
    
    
    public float force = 50.0f;
    public float minimumDistToAvoid = 20.0f;

    //Actual speed of the vehicle 
    private float curSpeed;
    private Vector3 targetPoint;
    private bool isObstacle=false;
    private float speed;
    // Use this for initialization
    void Start()
    {
        NPCController npc = gameObject.GetComponent<NPCController>();
        speed = npc.speed;
        
        targetPoint = Vector3.zero;
    }

      // Update is called once per frame
    void FixedUpdate()
    {
       

        //Vehicle move by mouse click
   //     RaycastHit hit;
     //   var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Directional vector to the target position
        Vector3 dir = (targetPoint - transform.position);
        dir.Normalize();

        //Apply obstacle avoidance
        AvoidObstacles(ref dir);
        if (isObstacle)
        {
            //Don't move the vehicle when the target point is reached
            if (Vector3.Distance(targetPoint, transform.position) < 3.0f)
                return;

            //Assign the speed with delta time
            curSpeed = speed * Time.deltaTime;

            //Rotate the vehicle to its target directional vector
            var rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5.0f * Time.deltaTime);

            //Move the vehicle towards
            transform.position += transform.forward * curSpeed;
        }
    }

    //Calculate the new directional vector to avoid the obstacle
    public void AvoidObstacles(ref Vector3 dir)
    {
        RaycastHit hit;

        //Only detect layer 8 (Obstacles)
        int layerMask = 1 << 8;

        //Check that the vehicle hit with the obstacles within it's minimum distance to avoid
        if (Physics.SphereCast(transform.position, 10.0f, transform.forward, out hit, minimumDistToAvoid, layerMask))
        {
            Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
            //Get the normal of the hit point to calculate the new direction
            Vector3 hitNormal = hit.normal;
            //  hitNormal.y = 0.0f; //Don't want to move in Y-Space

            //Get the new directional vector by adding force to vehicle's current forward vector
            //  if (hit.transform != this.transform)
            dir = transform.forward + hitNormal * force;
            isObstacle = true;
        }

        //if (Physics.SphereCast(transform.position, 10.0f, leftRay, out hit, minimumDistToAvoid, layerMask))
        //{
        //    //get the normal of the hit point to calculate the new direction
        //    Vector3 hitNormal = hit.normal;
        //    //  hitnormal.y = 0.0f; //don't want to move in y-space

        //    //get the new directional vector by adding force to vehicle's current forward vector
        //    if (hit.transform != this.transform)
        //    dir = transform.forward + hit.normal * force;
        //}

        //if (Physics.SphereCast(transform.position, 10.0f, rightRay, out hit, minimumDistToAvoid, layerMask))
        //{
        //    //get the normal of the hit point to calculate the new direction
        //    Vector3 hitNormal = hit.normal;
        //    //  hitnormal.y = 0.0f; //don't want to move in y-space

        //    //get the new directional vector by adding force to vehicle's current forward vector
        //    if (hit.transform != this.transform)
        //    dir = transform.forward + hit.normal * force;
        //}


    }
}