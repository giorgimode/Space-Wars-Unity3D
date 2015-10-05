using UnityEngine;
using System.Collections;

public class HitDetection : MonoBehaviour {
    public bool fixedLocation=false;
    public bool keyGuardian = false;
    private NPCController npc;
    private int rNum;
    private PlayerController scoreController;

    private GameObject objPlayer;
	// Use this for initialization

    protected void Initialize()
    {   
       
    
    }
	void Start () {
        npc = gameObject.GetComponent<NPCController>();
        rNum = Random.Range(1, 4);
      

         objPlayer = GameObject.FindGameObjectWithTag("Player");

        if (objPlayer != null)
        {
            scoreController = objPlayer.GetComponent<PlayerController>();
        }
        if (scoreController == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (fixedLocation && Vector3.Distance(transform.position, objPlayer.transform.position) > npc.chaseStartDist)
            npc.enabled = false;
        else npc.enabled = true;
	}

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("collder with player");


            DamageEffect(npc.health);

        }

        if (other.gameObject.tag == "Obstacle")
        {
            //get script info from other
            Obstacle projectile = other.gameObject.GetComponent<Obstacle>();
            if (projectile!=null)
            DamageEffect(projectile.damage);

        }

        if (other.gameObject.tag == "PlayerProjectile")
        {
            Debug.Log("detected player bullet" + other.gameObject.tag + " -- " + other.gameObject.transform.name);
            //get script info from other
            PlayerProjectile playerProjectile = other.gameObject.GetComponent<PlayerProjectile>();
            //   if (playerProjectile = null) Debug.Log("damage not found");

            DamageEffect(playerProjectile.damage);

        }

        if (other.gameObject.tag == "PlayerSuperBomb")
        {
            //get script info from other
            PlayerSuperBomb playerProjectile = other.gameObject.GetComponent<PlayerSuperBomb>();

            DamageEffect(playerProjectile.damage);

        }

      //  else Debug.Log("unkown object collision: " + other.gameObject.tag + " -- " + other.gameObject.transform.name);

    }



    public void DamageEffect(int damage)
    {
        Instantiate(npc.explosionAnimation, transform.position, transform.rotation);
        Instantiate(npc.explosion, transform.position, transform.rotation);

        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);

        //subtract other's projectile damage from health
        npc.health = npc.health - damage;
        if (npc.health <= 0f)
        {
            if (!keyGuardian)
            {
                if (rNum == 1)
                    Instantiate(npc.dropOnDeath1, transform.position, transform.rotation);
                else if (rNum == 2)
                    Instantiate(npc.dropOnDeath2, transform.position, transform.rotation);
                else if (rNum == 3)
                    Instantiate(npc.dropOnDeath3, transform.position, transform.rotation);
            }
            else Instantiate(npc.dropKey, transform.position, transform.rotation);

            Instantiate(npc.deathAnimation, transform.position, transform.rotation);
            Instantiate(npc.deathSound, transform.position, transform.rotation);
            Destroy(transform.gameObject);

            //	gameController.AddScore(scoreValue);

            Debug.Log("Switch to Dead State");
          //  npc.SetTransition(FSMTransition.NoHealth);
            
            scoreController.AddScore(npc.scoreValue);
            //				Destroy (other.gameObject);
            //				Destroy (gameObject);
        }
    }

}
