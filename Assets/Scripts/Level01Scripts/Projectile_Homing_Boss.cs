using UnityEngine;
using System.Collections;

public class Projectile_Homing_Boss : MonoBehaviour {
	public GameObject deathSound;
	public GameObject deathAnimiation;
	public float health;
	public float damage;
	public float speed;
	public float rotSpeed;
    public bool homing;
    private Quaternion targetRotation;
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
        targetRotation = Quaternion.LookRotation(target.position - transform.position);
    }
	// Update is called once per frame
	void Update ()
	{
        if (target != null && homing)
        {
						
						transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
						transform.Translate (Vector3.forward * Time.deltaTime * speed);
				}
        Destroy(gameObject, 5);

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
            if (deathSound!=null)
			Instantiate(deathSound, transform.position, transform.rotation);
            if (deathAnimiation!=null)
			Instantiate(deathAnimiation, transform.position, transform.rotation);
			Destroy (transform.gameObject);
		}
	}
}
