using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 6.0F;
	
	private Vector3 moveDirection = Vector3.zero;

	void Update() {

		moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("UpDown"), Input.GetAxis("Vertical"));
		
		moveDirection = transform.TransformDirection(moveDirection);
		
		moveDirection *= speed;

		CharacterController controller = GetComponent<CharacterController>();
		
		controller.Move(moveDirection * Time.deltaTime);
	}
}