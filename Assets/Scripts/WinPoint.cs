using UnityEngine;
using System.Collections;

public class WinPoint : MonoBehaviour {
	public GameObject explosionSound;
	public GameObject explosion;
	public GameObject explosionAnimation;
	


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider Others)
	{ //GameObject[] objects = GameObject.FindGameObjectsWithTag( "Enemy" );
		GameObject boss = GameObject.FindGameObjectWithTag( "Boss3" );
        if (Others.tag == "Player" && boss == null && gameObject.tag == "WinPoint")
        {

            Application.LoadLevel("StoryLine2");

		}
        //if (Others.tag == "Player")
        //{
        //    Debug.Log("win point");
        //    foreach( GameObject go in objects )
        //    {     


        //        Instantiate(explosionAnimation, transform.position, transform.rotation);
        //        //yield return new WaitForSeconds(0.2);
        //        Instantiate(explosionSound, transform.position, transform.rotation);
        //        //Instantiate(explosion, transform.position, transform.rotation);
        //        Destroy( go );
        //    }
			
        //}


	}



}
