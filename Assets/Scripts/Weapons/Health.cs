using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
[HideInInspector]
public float maxHealth = 100.0f;
public float health = 100.0f;

public float regenerateSpeed = 0.0f;
public int scoreValue;
   

public GameObject damagePrefab;
public Transform damageEffectTransform;
public float damageEffectMultiplier = 1.0f;
public bool  damageEffectCentered = true;
public float resetAfterDeathTime = 5f;				// How much time from the player dying to the level reseting.

public GameObject scorchMarkPrefab = null;
public AudioClip deathClip;							// The sound effect of the player dying.
[HideInInspector]
public bool invincible = false;
[HideInInspector]
public bool dead = false;

private GameObject scorchMark = null;

public SignalSender damageSignals;
public SignalSender dieSignals;

private float lastDamageTime = 0;
private ParticleEmitter damageEffect;
private float damageEffectCenterYOffset;

private float colliderRadiusHeuristic = 1.0f;
private float timer;
private DoneSceneFadeInOut sceneFadeInOut;			// Reference to the SceneFadeInOut script.
private bool playerEnd = false;
private AudioSource gameTheme1;
private AudioSource gameTheme2;
private GameObject[] sirenGameObjects;
private SpawnAtCheckpoint scoreController;
private bool checkLives = false;
private PlayerLives playerLives;
//private int lives;
void  Awake (){
	//enabled = false;
	if (damagePrefab) {
		if (damageEffectTransform == null)
			damageEffectTransform = transform;
		GameObject effect = Spawner.Spawn (damagePrefab, Vector3.zero, Quaternion.identity);
		effect.transform.parent = damageEffectTransform;
		effect.transform.localPosition = Vector3.zero;
		damageEffect = effect.GetComponent<ParticleEmitter>();
		Vector2 tempSize = new Vector2(GetComponent<Collider>().bounds.extents.x,GetComponent<Collider>().bounds.extents.z);
		colliderRadiusHeuristic = tempSize.magnitude * 0.5f;
		damageEffectCenterYOffset = GetComponent<Collider>().bounds.extents.y;

	}
	if (scorchMarkPrefab) {
		scorchMark = GameObject.Instantiate(scorchMarkPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		scorchMark.SetActive (false);
	}
	sceneFadeInOut = GameObject.FindGameObjectWithTag(DoneTags.fader).GetComponent<DoneSceneFadeInOut>();
    gameTheme1 = GameObject.FindGameObjectWithTag("AlarmSound").GetComponent<AudioSource>();
    Debug.Log(gameTheme1.name);
    gameTheme2 = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>();
   sirenGameObjects = GameObject.FindGameObjectsWithTag(DoneTags.siren);
   
   // if (gameTheme != null) Debug.Log("music found");

   scoreController = GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnAtCheckpoint>();
   checkLives = false;
   playerLives = GameObject.FindGameObjectWithTag("PlayerLives").GetComponent<PlayerLives>();
 //  lives = playerLives.lives;
}

void Update()
{
    if (gameObject.tag == "Player" && health <= 0)
    {
        
        GameScore.RegisterDeath(gameObject);

        health = 0;
        dead = true;
        // Play the dying sound effect at the player's location.

        if (!checkLives) playerLives.lives -= 1; 
        
        checkLives = true;

        //If the timer is greater than or equal to the time before the level resets...
        if (!playerEnd)
        {
            AudioSource.PlayClipAtPoint(deathClip, transform.position);
            playerEnd = true;
            dieSignals.SendSignals(this);
        }

        // Stop the footsteps playing.
        GetComponent<AudioSource>().Stop();
        if (damageEffect)
        {
            damageEffect.transform.rotation = transform.rotation;
            if (!damageEffectCentered)
            {
                //Vector3 dir = fromDirection;
                //dir.y = 0.0f;
                damageEffect.transform.position = (transform.position + Vector3.up * damageEffectCenterYOffset);
            }
            damageEffect.GetComponent<EllipsoidParticleEmitter>().maxSize = 5;
            damageEffect.Emit();// (particleAmount);
            //  GameObject a = damageEffect.GetComponent<EllipsoidParticleEmitter>();

            gameTheme1.enabled = false;
            gameTheme2.enabled = false;
            if (playerLives.lives > 0)
                LevelReset(false);
            else LevelReset(true);

            // Destroy(gameObject);
        }

        foreach (GameObject siren in sirenGameObjects)
            siren.GetComponent<AudioSource>().mute = true;

        
    }
}

public void  OnDamage ( float amount ,   Vector3 fromDirection  ){
	// Take no damage if invincible, dead, or if the damage is zero
	if(invincible)
		return;
	if (dead)
		return;
	if (amount <= 0)
		return;


	health -= amount;
	damageSignals.SendSignals (this);
	lastDamageTime = Time.time;

	// Enable so the Update function will be called
	// if regeneration is enabled
    //if (regenerateSpeed > 0)
    //    enabled = true;

	// Show damage effect if there is one
	if (damageEffect) {
		damageEffect.transform.rotation = Quaternion.LookRotation (fromDirection, Vector3.up);
		if(!damageEffectCentered) {
			Vector3 dir = fromDirection;
			dir.y = 0.0f;
			damageEffect.transform.position = (transform.position + Vector3.up * damageEffectCenterYOffset) + colliderRadiusHeuristic * dir;
		}

		damageEffect.Emit();// (particleAmount);
      //  GameObject a = damageEffect.GetComponent<EllipsoidParticleEmitter>();
	}

    //// Die if no health left
    if (health <= 0 && gameObject.tag!="Player")
    {
        GameScore.RegisterDeath(gameObject);

        health = 0;
        dead = true;
        dieSignals.SendSignals(this);
        //	enabled = false;
        //   Destroy(gameObject);
        //  LevelReset();
        // scorch marks
        if (scorchMark)
        {
            scorchMark.SetActive(true);

            Vector3 scorchPosition = GetComponent<Collider>().ClosestPointOnBounds(transform.position - Vector3.up * 100);
            scorchMark.transform.position = scorchPosition + Vector3.up * 0.1f;
            //scorchMark.transform.eulerAngles.y = Random.Range (0.0f, 90.0f);
            scorchMark.transform.eulerAngles = new Vector3(0.0f, Random.Range(0.0f, 90.0f), 0.0f);
        }
        if (scoreController != null)
            scoreController.AddScore(scoreValue);
    }
}

//void  OnEnable (){
//    Regenerate ();
//}

// Regenerate health

public IEnumerator  Regenerate (){
	if (regenerateSpeed > 0.0f) {
		while (enabled) {
			if (Time.time > lastDamageTime + 3) {
				health += regenerateSpeed;

				yield return 0;

				if (health >= maxHealth) {
					health = maxHealth;
					enabled = false;
				}
			}
			yield return new WaitForSeconds (1.0f);
		}
	}
}


void LevelReset(bool gameOver)
{
	// Increment the timer.
//	timer += Time.deltaTime;

	//If the timer is greater than or equal to the time before the level resets...
	//if (timer >= resetAfterDeathTime)
		// ... reset the level.
   
    sceneFadeInOut.EndScene(gameOver);
}
}