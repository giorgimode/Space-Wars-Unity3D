using UnityEngine;
using System.Collections;

public class FinalKeyPickUp : MonoBehaviour {

    public AudioClip keyGrab;							// Audioclip to play when the key is picked up.
    public float speed = 4;
    private GameObject player;				// Reference to the player.
    private DonePlayerInventory playerInventory;		// Reference to the player's inventory.
	

    void Awake()
    {
        // Setting up the reference.
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        playerInventory = player.GetComponent<DonePlayerInventory>();
    }

    void OnTriggerStay(Collider other)
    {
        // If the colliding gameobject is the player...
        if (other.gameObject == player)
            // ... and the switch button is pressed...
            if (Input.GetButton("Switch"))
                // ... deactivate the laser.
                PickUpFinalKey();
    }

	void Update()
    {
        transform.Rotate(0.0f,Time.deltaTime * speed, 0.0f);


    }
	// Update is called once per frame
    void PickUpFinalKey()
    {
        // ... play the clip at the position of the key...
        AudioSource.PlayClipAtPoint(keyGrab, transform.position);
        playerInventory.hasFinalKey = true;
        Destroy(gameObject);
    }
}
