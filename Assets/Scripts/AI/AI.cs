using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {


// Public member data
public MonoBehaviour behaviourOnSpotted;
public AudioClip soundOnSpotted;
public MonoBehaviour behaviourOnLostTrack;
public float activationDistance = 7;
// Private memeber data
private Transform character;
private Transform player;
private bool  insideInterestArea = true;
private DoneLastPlayerSighting lastPlayerSighting;
private EnemySight enemySight;
private float playerHealth;
void  Awake (){
    character = transform;
    player = GameObject.FindWithTag ("Player").transform;
    lastPlayerSighting = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<DoneLastPlayerSighting>();
    enemySight = GameObject.FindGameObjectWithTag("AIattack").GetComponent<EnemySight>();
   
}

void  OnEnable (){
    behaviourOnLostTrack.enabled = true;
    behaviourOnSpotted.enabled = false;
}

void Update()
{   
    playerHealth = player.GetComponent<Health>().health;
    if (player != null && playerHealth > 0 && (Vector3.Distance(transform.position, player.position) < activationDistance || enemySight.personalLastSighting != lastPlayerSighting.resetPosition))
   OnSpotted(); 
    else OnEnable(); 
}
//void  OnTriggerEnter ( Collider other  ){
//    if (other.transform == player && CanSeePlayer ()) {
//        OnSpotted ();
//    }
//}

public void OnEnterInterestArea()
{
    insideInterestArea = true;
}

public void OnExitInterestArea()
{
    insideInterestArea = false;
    OnLostTrack ();
}

public void OnSpotted()
{
    if (!insideInterestArea)
        return;
    if (!behaviourOnSpotted.enabled) {
        lastPlayerSighting.position = player.transform.position;
        behaviourOnSpotted.enabled = true;
        behaviourOnLostTrack.enabled = false;
        
        if (GetComponent<AudioSource>() && soundOnSpotted) {
            GetComponent<AudioSource>().clip = soundOnSpotted;
            GetComponent<AudioSource>().Play ();
            
        }
    }
     

}

public void  OnLostTrack (){
    if (!behaviourOnLostTrack.enabled) {
        behaviourOnLostTrack.enabled = true;
        behaviourOnSpotted.enabled = false;
    }
}

public bool CanSeePlayer (){
     Vector3 playerDirection = (player.position - character.position);
    RaycastHit hit;
    Physics.Raycast (character.position, playerDirection, out hit, playerDirection.magnitude);
    if (hit.collider && hit.collider.transform == player) {
        return true;
    }
    return false;
}

}