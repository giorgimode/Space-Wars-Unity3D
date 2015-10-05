using UnityEngine;
using System.Collections;

public class SimpleRotate : MonoBehaviour {

public float speed = 4.0f;


void  Update (){
	transform.Rotate(0.0f, 0.0f, Time.deltaTime * speed);
}
}