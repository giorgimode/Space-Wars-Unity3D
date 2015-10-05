using UnityEngine;
using System.Collections;

public class Boss3 : MonoBehaviour
{

    public GameObject deathSound;
    public GameObject deathAnimation1;
    public GameObject deathAnimation2;
    public GameObject deathAnimation3;

    public GameObject kami;
    public GameObject flame;
    public GameObject lasers;
    public GameObject homingMissiles;

    public Transform kamiSpawn1;
    public Transform kamiSpawn2;
    public Transform kamiSpawn3;

    public Transform flameSpawn;

    public Transform shotSpawn1;
    public Transform shotSpawn2;
    public Transform shotSpawn3;
    public Transform shotSpawn4;

    public Transform missileSpawn1;
    public Transform missileSpawn2;
    public Transform missileSpawn3;
    public Transform missileSpawn4;

    public float health;
    public float attackRange;
    public float kamiNextSpawn;
    public float kamiSpawnRate;
    public float flameNextFire;
    public float flameFireRate;
    public float laserNextFire;
    public float laserFireRate;
    public float missileNextFire;
    public float missileFireRate;

    public Transform target;
    private float distance;
    private int rNum;
    private Done_GameController gameController;
   // private bool attack = true;
  //  private bool firstEncounter = true;
   // private bool dead = false;

    void Start()
    {   // FIX ADD DESTROY BY BOUNDARY/TIME

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
    IEnumerator expl2()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(deathAnimation2, transform.position, transform.rotation);
    }

    IEnumerator expl3()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(deathAnimation3, transform.position, transform.rotation);
    }

    //	void Awake()
    //	{
    //		target = GameObject.FindGameObjectWithTag ("Player").transform;
    //	}

    // Update is called once per frame
    void Update()
    {
       
            distance = Vector3.Distance(transform.position, target.position);

            //if (!dead)
            //{
               

                if (distance <= attackRange)
                {
                    gameController.bossFight = true;
                    transform.LookAt(target);

                    if (Time.time > kamiNextSpawn)
                    {
                        kamiNextSpawn = Time.time + kamiSpawnRate;
                        Instantiate(kami, kamiSpawn1.position, kamiSpawn1.rotation);
                        Instantiate(kami, kamiSpawn2.position, kamiSpawn2.rotation);
                        Instantiate(kami, kamiSpawn3.position, kamiSpawn3.rotation);
                       
                    }

                    if (Time.time > laserNextFire)
                    {
                        flameNextFire = Time.time + flameFireRate;
                        Instantiate(flame, flameSpawn.position, flameSpawn.rotation);
                        Debug.Log("flame");
                    }

                    if (Time.time > laserNextFire)
                    {
                        laserNextFire = Time.time + laserFireRate;
                        Instantiate(lasers, shotSpawn1.position, shotSpawn1.rotation);
                        Instantiate(lasers, shotSpawn2.position, shotSpawn2.rotation);
                        Instantiate(lasers, shotSpawn3.position, shotSpawn3.rotation);
                        Instantiate(lasers, shotSpawn4.position, shotSpawn4.rotation);
                    }

                    if (Time.time > missileNextFire)
                    {
                        missileNextFire = Time.time + missileFireRate;
                        rNum = Random.Range(1, 5);
                        switch (rNum)
                        {
                            case 1:
                                Instantiate(homingMissiles, missileSpawn1.position, missileSpawn1.rotation);
                                break;
                            case 2:
                                Instantiate(homingMissiles, missileSpawn2.position, missileSpawn2.rotation);
                                break;
                            case 3:
                                Instantiate(homingMissiles, missileSpawn3.position, missileSpawn3.rotation);
                                break;
                            case 4:
                                Instantiate(homingMissiles, missileSpawn4.position, missileSpawn4.rotation);
                                break;
                        }
                    }
                }
            }
        
   // }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "PlayerProjectile")
        {
            //get script info from other
            PlayerProjectile playerProjectile = other.gameObject.GetComponent<PlayerProjectile>();

            ProjectileEffect(playerProjectile.damage);

        }

        if (other.gameObject.tag == "PlayerSuperBomb")
        {
            //get script info from other
            PlayerSuperBomb playerProjectile = other.gameObject.GetComponent<PlayerSuperBomb>();

            ProjectileEffect(playerProjectile.damage);

        }
    }

    private void ProjectileEffect(int damage)
    {
       // firstEncounter = false;

        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);

        //subtract other's projectile damage from health
        health = health - damage;

        if (health <= 0f)
        {

           // attack = false;
      //      dead = true;

            Instantiate(deathSound, transform.position, transform.rotation);
            Destroy(transform.gameObject, 4f);

            Instantiate(deathAnimation1, transform.position, transform.rotation);
            //play next explosions so they are in synce with the audio
            StartCoroutine(expl2());
            StartCoroutine(expl3());
        }
    }
}
