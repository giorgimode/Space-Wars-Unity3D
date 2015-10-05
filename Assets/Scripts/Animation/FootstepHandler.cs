using UnityEngine;
using System.Collections;

public class FootstepHandler : MonoBehaviour {

//public enum FootType {
//    Player,
//    Mech,
//    Spider
//}

//public AudioSource audioSource;
//public FootType footType;

//private PhysicMaterial physicMaterial;

//public void OnCollisionEnter(Collision collisionInfo)
//{
//    physicMaterial = collisionInfo.collider.sharedMaterial;
//}

void Update()
{


    if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
    {
        if (!GetComponent<AudioSource>().isPlaying)
            // ... play them.
            GetComponent<AudioSource>().Play();
    }
}
//public void OnFootstep()
//{
//    if (!audioSource.enabled)
//    {
//        return;
//    }
	
//    AudioClip sound = null;
//    switch (footType) {

//    case FootType.Player:
//        sound = MaterialImpactManager.GetPlayerFootstepSound (physicMaterial);
//        break;
//    case FootType.Mech:
//        sound = MaterialImpactManager.GetMechFootstepSound (physicMaterial);
//        break;
//    case FootType.Spider:
//        sound = MaterialImpactManager.GetSpiderFootstepSound (physicMaterial);
//        break;
//    }	
//    audioSource.pitch = Random.Range (0.98f, 1.02f);
//    audioSource.PlayOneShot (sound, Random.Range (0.8f, 1.2f));
//}

}
