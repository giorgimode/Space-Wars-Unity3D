using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

	public float damage;
	public float speed;
    private Done_GameController gameController;
	void Start ()
	{   // FIX ADD DESTROY BY BOUNDARY/TIME
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<Done_GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
	}

    void Update()
    {
        if (gameController.bossFight) Destroy(gameObject, 10);
        else Destroy(gameObject, 5);
    }

	 void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Enemy") {
		}
        else if (other.gameObject.tag == "EnemyProjectile")
        {
		}
        else if (other.gameObject.tag == "EnemyProjectileHoming")
        {
		}
        else if (other.gameObject.tag == "PickUp")
        {
		}
        else if (other.gameObject.tag == "Boss3")
        {
		}
        else if (other.gameObject.tag == "CheckPoint")
        {
		}
        else if (other.gameObject.tag == "FinalBattle")
        {
        }
        else if (other.gameObject.tag == "WinPoint")
        {
        }
		else {
			Destroy (transform.gameObject);
		}
	}
}
