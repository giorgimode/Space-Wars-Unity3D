using UnityEngine;
using System.Collections;

public class Cockpit : MonoBehaviour {
	
	public Texture2D cockpitTexture;
	Rect position;
	public bool showCockpit = false;
	public GameObject shotSpot;
	void Update()
	{
		position = new Rect ((Screen.width - Screen.width), (Screen.height - Screen.height), Screen.width, Screen.height);
	}
	
	void OnGUI()
	{
		Cursor.visible = false;
		if (showCockpit == true) {
			GUI.DrawTexture(position, cockpitTexture);
//			Vector3 temp = new Vector3(0,10.0f,0);
//			shotSpot.transform.position = shotSpot.transform.position.y + 10.0f;

		}
		/*
		PlayerController pc = transform.parent.GetComponent<PlayerController> ();
		if (pc.beenHit)
			GUI.DrawTexture (position, hitTexture);*/
	}


}
