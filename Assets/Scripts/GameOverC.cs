using UnityEngine;
using System.Collections;

public class GameOverC : MonoBehaviour {

	public void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("StartMenu");
		}
	}
}
