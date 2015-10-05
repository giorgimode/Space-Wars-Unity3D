using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    //screen graphics
    public Texture2D hitTexture;
    public Texture2D lifeImage;
    public Texture2D yellowKey;
    public Texture2D purpleKey;
    public GameObject laserDestroyed;
    //place weapon prefabs into the script
    public GameObject lasers;
    public GameObject flameThrower;
    //public GameObject lightningGun;
    public GameObject missiles;
    //public GameObject bombs;
    public GameObject superBomb;

    public Transform shotSpawn; //location where projectile spawns relative to the player controller
    [HideInInspector]
    public float primaryNextFire;
     [HideInInspector]
    public float SecondaryNextFire;

    public float health;
    public int lives = 2;
    public float fireRate;
    public float flamerThrowerFireRate;
    public float hitDuration;

    public GUIStyle myStyle; //make adjustments to GUI text
    //set each weapon to true as it is unlocked
 //   private bool availableFlameThrower = true;
    //private bool availableLightningGun = false;
    //private bool availableBombs = false;

    //status indicators
    private bool beenHit = false;

    private int primaryWeaponSelected = 1; //laser-1, flame thrower-2, lightning gun-3
    private int secondaryWeaponSelected = 1; //missiles-4, bombs-5

    //ammo count of each projectile
    private int ammoFlameThrower = 100;
    //private int ammoLightningGun = 0;
    private int ammoMissiles = 10;
    //private int ammoBombs = 0;
    private int ammoSuperBombs = 3;
    //private int scraps = 0;

    //Gate keys - 1 have, 0 don't have
    private int key1 = 0;
    private int key2 = 0;
    //public GUIText scoreText;
    //	public GUIText restartText;
    //public GUIText gameOverText;

    private bool gameOver;
    //private bool restart;
    private int score;
    private Transform lastCheckpoint; //where player spawns on death
    private float fullHealth;
    private bool paused = false;
    private bool dead = false;
    private GameObject levelPart1;
    private GameObject levelPart2;

    private Renderer[] renderers2, renderers1;

    IEnumerator hitScreenDuration()
    {
        yield return new WaitForSeconds(hitDuration);
        beenHit = false;
    }
    void Start()
    {   
        fullHealth = health;
        //gameOver = false;
        //restart = false;
        //		restartText.text = "";
        //gameOverText.text = "";
        score = 0;
       // UpdateScore();
        levelPart1 = GameObject.FindGameObjectWithTag("Part1");
        levelPart2 = GameObject.FindGameObjectWithTag("Part2");
        renderers2 = levelPart2.GetComponentsInChildren<Renderer>();
        renderers1 = levelPart1.GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers2)
        {
            r.enabled = false;
        }
        //StartCoroutine (SpawnWaves ());
    }
    // Update is called once per frame
    void Update()
    {  
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                paused = false;
                Time.timeScale = 1;

            }
            else
            {
                paused = true;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
        }
       
        if (Input.GetButton("Fire1") && Time.time > primaryNextFire)
        {
            primaryNextFire = Time.time + fireRate;
            Instantiate(lasers, shotSpawn.position, shotSpawn.rotation);

        }

        //weapons switching
        if (Input.GetKeyDown(KeyCode.Alpha1))
            secondaryWeaponSelected = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            secondaryWeaponSelected = 2;
        /*if (Input.GetKeyDown (KeyCode.Alpha3) && availableLightningGun)
                        primaryWeaponSelected = 3;*/

       
        switch (secondaryWeaponSelected)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > SecondaryNextFire && ammoMissiles > 0)
                {
                 //   SecondaryNextFire = Time.time + fireRate;
                    Instantiate(missiles, shotSpawn.position, shotSpawn.rotation);
                    ammoMissiles--;
                }
                break;
          
            case 2:
                if (Input.GetKey(KeyCode.Mouse1) && Time.time > SecondaryNextFire && ammoFlameThrower > 0)
                {
                 //   SecondaryNextFire = Time.time + flamerThrowerFireRate;
                    Instantiate(flameThrower, shotSpawn.position, shotSpawn.rotation);
                    ammoFlameThrower--;
                }
                break;
            /*case 5:
            if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > SecondaryNextFire)
            {
                SecondaryNextFire = Time.time + fireRate;
                Instantiate (lightningGun, shotSpawn.position, shotSpawn.rotation);
            }
            break;
        case 6:
            if (Input.GetButton ("Fire1") && Time.time > SecondaryNextFire)
            {
                SecondaryNextFire = Time.time + fireRate;
                Instantiate (lightningGun, shotSpawn.position, shotSpawn.rotation);
            }
            break;*/
        }

        //fires super bomb
        if (Input.GetKeyDown(KeyCode.B) && ammoSuperBombs > 0)
        {
            Instantiate(superBomb, transform.position, transform.rotation);
            ammoSuperBombs--;
        }

    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
       
    }

    //void UpdateScore()
    //{
    //    //scoreText.text = "Score: " + score;
    //    //	GUI.Label (new Rect ((Screen.width-120)/2, 0, 120, 20), "Score: " + score, mystyle);

    //}
    void OnGUI()
    {
        
        //Counts displaed on HUD
        GUI.Label(new Rect(Screen.width / 1.4f, 0, 160, 110), "ARMOR ", myStyle); 
        GUI.Label(new Rect(Screen.width / 6f, 0, 160, 110), "SCORE: " + score, myStyle);
        //GUI.Label(new Rect((Screen.width - 120) / 2 + 200, 0, 120, 20), "Lives: " + lives, myStyle);
       for (int i = 1; i <=lives; i++)
                 GUI.DrawTexture(new Rect(Screen.width/100 +i/1.1f*lifeImage.width, Screen.height - lifeImage.height*2, yellowKey.width / 1.5f, yellowKey.height / 1.5f), lifeImage);

        GUI.Label(new Rect((Screen.width - 130), (Screen.height) / 1.26f, 120, 20), "Flame: " + ammoFlameThrower, myStyle);
        GUI.Label(new Rect((Screen.width - 130), (Screen.height)/1.18f, 120, 20), "Missiles : " + ammoMissiles, myStyle);
        GUI.Label(new Rect((Screen.width - 130), (Screen.height)/1.1f, 120, 20), "Superbombs: " + ammoSuperBombs, myStyle);
        //GUI.Label (new Rect (0, 0, 120, 20), "Scraps: " + scraps, myStyle);
        if (paused && !dead)
        {
            GUI.skin.button.fontSize = Screen.width / 20;
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height / 2 - 15, 500, 130), "Exit to menu"))
            {
                Application.LoadLevel("StartMenu");
                paused = false;
                Time.timeScale = 1;
            }
        }

        if (dead && lives > 0)
        {
            GUI.skin.button.fontSize = Screen.width / 80;
            GUI.skin.button.fontStyle = FontStyle.Bold;
            if (GUI.Button(new Rect(Screen.width / 2-60, Screen.height / 2, 180, 90), "Exit to Menu"))
            {
                Application.LoadLevel("StartMenu");
                dead = false;
                Time.timeScale = 1;
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 4, 180, 90), "Last Checkpoint"))
            {
               // Application.LoadLevel("StartMenu");
                dead = false;
                //checkpointOn = true;
                lives--;
                health = fullHealth;
                transform.position = lastCheckpoint.position;
                transform.rotation = lastCheckpoint.rotation;

            
                Time.timeScale = 1;
            }
            //else if (lives <= 0)
            //{               
                
            //    Application.LoadLevel("StartMenu");
            //    Time.timeScale = 1;
            //}

        }


        //key cards
        if (key1 >= 1)
            GUI.DrawTexture(new Rect(Screen.width - yellowKey.width, (Screen.height / 2) - (yellowKey.height / 2), yellowKey.width, yellowKey.height), yellowKey);

        if (key2 >= 1)
            GUI.DrawTexture(new Rect(Screen.width - purpleKey.width, (Screen.height / 2) - 10, purpleKey.width, purpleKey.height), purpleKey);

        //hit indicator
        if (beenHit)
            GUI.DrawTexture(new Rect((Screen.width - Screen.width), (Screen.height - Screen.height), Screen.width, Screen.height), hitTexture);

    }


    void OnCollisionEnter(Collision other)
    {
       
        if (other.gameObject.tag == "Obstacle")
        {
            beenHit = true;
            StartCoroutine(hitScreenDuration());


            Obstacle proj = other.gameObject.GetComponent<Obstacle>();
            if (proj != null)
                health = health - proj.damage;
            //  Debug.Log("testing2--" + proj.damage);
            CheckPlayerLife();

        }

        //add pickup to inventory
        if (other.gameObject.tag == "PickUp")
        {

            PickUps pickup = other.gameObject.GetComponent<PickUps>();

            health += pickup.health;
            ammoFlameThrower += pickup.flameThrower;
            //ammoLightningGun += pickup.lightningGun;
            ammoMissiles += pickup.missiles;
            ammoSuperBombs += pickup.superBombs;
            //scraps += pickup.metalScraps;
            key1 += pickup.gateKey1;
            key2 += pickup.gateKey2;
        }

        //hit by an enemy projectile
        if (other.gameObject.tag == "EnemyProjectile")
        {

            beenHit = true;
            StartCoroutine(hitScreenDuration());

            EnemyProjectile enemyProjectile = other.gameObject.GetComponent<EnemyProjectile>();

            health = health - enemyProjectile.damage;

            CheckPlayerLife();
        }

        //hit by an enemy homing missile
        if (other.gameObject.tag == "EnemyProjectileHoming")
        {

            beenHit = true;
            StartCoroutine(hitScreenDuration());

            Enemy_Projectile_Homing enemyProjectileHoming = other.gameObject.GetComponent<Enemy_Projectile_Homing>();

            health = health - enemyProjectileHoming.damage;

            CheckPlayerLife();
        }
        //		if (other.tag == "EnemyProjectile") {
        //			
        //			beenHit = true;
        //			StartCoroutine(hitScreenDuration());
        //			
        //			EnemyProjectile enemyProjectile = other.GetComponent<EnemyProjectile>();
        //			
        //			health = health - enemyProjectile.damage;
        //			
        //			if(health <= 0f && lives > 0){
        //				lives--;
        //				health = 100f;
        //				transform.position = lastCheckpoint.position;
        //				transform.rotation = lastCheckpoint.rotation;
        //			}
        //		}
        if (other.gameObject.tag == "Laser")
        {

            beenHit = true;
            StartCoroutine(hitScreenDuration());

            Laserhit laser1 = other.gameObject.GetComponent<Laserhit>();


            health = health - laser1.damage;

            CheckPlayerLife();
        }
        if (other.gameObject.tag == "Laser2")
        {

            beenHit = true;
            StartCoroutine(hitScreenDuration());


            Laserhit2 laser2 = other.gameObject.GetComponent<Laserhit2>();
            health = health - laser2.damage;

            CheckPlayerLife();
        }
        //hit by an enemy kamaikazee explosion
        if (other.gameObject.tag == "EnemyKamikazee")
        {

            beenHit = true;
            StartCoroutine(hitScreenDuration());

            Enemy_Kamikazee_Explosion enemyKami = other.gameObject.GetComponent<Enemy_Kamikazee_Explosion>();

            health = health - enemyKami.damage;

            CheckPlayerLife();
        }

        //sets new checkpoint and disables old one
        if (other.gameObject.tag == "CheckPoint")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());

            Debug.Log("checkpoint detected");
            foreach (Transform child in other.transform)
            {
                lastCheckpoint = child.transform;
               
            }
            
            other.gameObject.transform.gameObject.SetActive(false);



            if (other.gameObject.name == "Checkpoint2")
            {
                foreach (Renderer r in renderers2)
                {
                    r.enabled = true;
                }

                levelPart1.SetActive(false);
            }

            if (other.gameObject.name == "Checkpoint3")
            {
                levelPart2.SetActive(false);
            }
        }

        //opens gate if key has been acquired
        if (other.gameObject.tag == "LaserWall" && (key1 >= 1 || key2 >= 1))
        {   
            Instantiate(laserDestroyed, transform.position, transform.rotation);
            other.gameObject.SetActive(false);
            key1 = 0;
            key2 = 0;
            //foreach (Transform child in other.transform)     
            //{  
            //    child.gameObject.SetActiveRecursively(false);   
            //}

        }
    }



    void CheckPlayerLife()
    {
        if (health <= 0f && lives > 0)
        {
            dead = true;
            Time.timeScale = 0;
     }
        else if (health <= 0f && lives == 0)
             Application.LoadLevel("GameOver");
        else dead=false;
        
         // might not work if gate isTriggerer

            var renderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                if (r.name == "Smoke" && health >= 0f && health < fullHealth / 4) r.enabled = true;
                else   if (r.name == "Smoke" && (health < 0f || health > fullHealth / 4)) r.enabled = false;          
            }
    
        



    }
}