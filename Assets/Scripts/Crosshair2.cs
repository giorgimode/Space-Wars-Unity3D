using UnityEngine;
using System.Collections;

public class Crosshair2 : MonoBehaviour {
	
	public Texture2D crosshairTexture;
	Rect position;
	public bool showCrosshair = true;
	
	void Update()
	{
		position = new Rect ((Screen.width - crosshairTexture.width) / 2 -5, (Screen.height - crosshairTexture.height) / 2 - 65, crosshairTexture.width, crosshairTexture.height);
	}
	
	void OnGUI()
	{
		Cursor.visible = false;
		if (showCrosshair == true) {
			//GUI.DrawTexture(new Rect((Screen.width - crosshairTexture.width), (Screen.height - crosshairTexture.height), crosshairTexture.width, crosshairTexture.height), crosshairTexture);
			GUI.DrawTexture(position, crosshairTexture);
		}
	}
}
