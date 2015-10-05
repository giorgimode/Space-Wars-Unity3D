using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	
	private Transform playerLocation;		// Reference to the player.
	private GameObject player;
	
	void Awake ()
	{
		// Setting up the reference.
		playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update ()
	{
		// Set the position to the player's position with the offset.
		if (player != null)	transform.position = playerLocation.position + offset;
	}
}
