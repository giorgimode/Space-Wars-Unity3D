using UnityEngine;
using System.Collections;

public class Enemy_Projectile_Homing : MonoBehaviour {

	public GameObject deathSound;
	public GameObject deathAnimiation;
	public float health;
	public float damage;
	public float speed;
	public float rotSpeed;
    public bool homing;
    private Done_GameController gameController;
	private Transform target;
	
	void Awake()
	{  // if (target != null)
        // FIX ADD DESTROY BY BOUNDARY/TIME
        if (homing)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            if (target == null)
            {
                Debug.Log("Target not found");
                return;
            }
            else target = GameObject.FindGameObjectWithTag("Player").transform;
        }
	}

    void Start()
    {   // FIX ADD DESTROY BY BOUNDARY/TIME
        if (!homing)
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
	// Update is called once per frame
	void Update ()
	{
        if (target != null && homing)
        {
						Quaternion targetRotation = Quaternion.LookRotation (target.position - transform.position);
						transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
						transform.Translate (Vector3.forward * Time.deltaTime * speed);
				}
        if     (gameController.bossFight)   Destroy(gameObject, 10);
        else Destroy(gameObject, 5);

	}


    void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.tag == "Enemy") {
		}
        else if (other.gameObject.tag == "EnemyProjectile")
        {
		}
        else if (other.gameObject.tag == "PickUp")
        {
		}
        else if (other.gameObject.tag == gameObject.tag)
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
        //else if(other.tag == "PlayerProjectile"){
        //    PlayerProjectile pp = other.GetComponent<PlayerProjectile>();
        //    health = health - pp.damage;
        //    if(health <= 0){
        //        Instantiate(deathSound, transform.position, transform.rotation);
        //        Instantiate(deathAnimiation, transform.position, transform.rotation);
        //        Destroy(transform.gameObject);
        //    }
        //}
		else {
			Instantiate(deathSound, transform.position, transform.rotation);
			Instantiate(deathAnimiation, transform.position, transform.rotation);
			Destroy (transform.gameObject);
		}
	}
}
