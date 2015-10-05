using UnityEngine;
using System.Collections;

public class MuzzleFlashAnimate : MonoBehaviour {

void  Update (){
	transform.localScale = Vector3.one * Random.Range(0.5f,1.5f);
	//transform.localEulerAngles.z = Random.Range(0,90.0f);
    transform.localEulerAngles = new Vector3(0.0f, 0.0f, Random.Range(0, 90.0f));
}
}
