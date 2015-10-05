using UnityEngine;
using System.Collections;

//[System.Serializable]
//public class Done_Boundary 
//{
//	public float xMin, xMax, zMin, zMax;
//}

public class Done_PlayerFlyer : MonoBehaviour
{    //place weapon prefabs into the script
    public float speed;
    public int tiltSpeed; // Q/E
    //public Done_Boundary boundary;

    public float mouseRotation = 10;  // up down mousit
    //   public float leftRightRotation = 10; // triali gverdze
  //  Vector3 moveDir = Vector3.zero;
    void FixedUpdate()
    {
       // Vector3 movement;
        float rotateLeftRight = Input.GetAxis("Sides") ; //rotate on x axis 
        float moveForward = Input.GetAxis("Vertical") ;
        float side = Input.GetAxis("Horizontal"); //move left or right
        float rotateUpDown = Input.GetAxis("Mouse ScrollWheel"); // rotate upward or downward
        float tilt = Input.GetAxis("Tilt"); //rotate on z axis

        if (Input.anyKey)
        transform.Translate(side * speed/10, 0, moveForward* speed/10);
        else GetComponent<Rigidbody>().velocity = Vector3.Slerp(GetComponent<Rigidbody>().velocity, Vector3.zero, 5.0f);
            

     
        //smooth movement using mouse 
        GetComponent<Rigidbody>().AddRelativeTorque(rotateUpDown * mouseRotation, 0, 0);  // rotate up down **************************
        GetComponent<Rigidbody>().AddRelativeTorque(0, rotateLeftRight * mouseRotation, 0);  // rotate up down **************************
        GetComponent<Rigidbody>().AddRelativeTorque(0, 0, tilt * tiltSpeed);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * speed * 1.5f);
        }
    }
}
