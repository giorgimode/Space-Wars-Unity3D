//@script ExecuteInEditMode()
public var intro : String[];
public var off : float;
public var speed = 100;
var gSkin : GUISkin;
var backdrop : Texture2D;
private var isLoading = false;
function OnGUI()
{
if(gSkin)
GUI.skin = gSkin;
else
Debug.Log("StartMenuGUI : GUI Skin object missing!");
var backgroundStyle : GUIStyle = new GUIStyle();
backgroundStyle.normal.background = backdrop;
GUI.Label ( Rect( ( Screen.width - (Screen.height * 2)) * 0.75, 0, Screen.height * 2,
Screen.height), "", backgroundStyle);

off += (Time.deltaTime*100);
//GUI.Label(new Rect(0,-1000 + off,Screen.width, 1000),"Some long text");
GUI.Label ( Rect( (Screen.width/2)-197, 50 +off, 800, 200 ), "SPACE     RESCUE",
"mainMenuTitle");



//if (GUI.Button( Rect( (Screen.width/2)-70, Screen.height -160, 140, 70), "Play"))
//{
//isLoading = true;
//Application.LoadLevel("Level2");
//}
var isWebPlayer = (Application.platform == RuntimePlatform.OSXWebPlayer ||
Application.platform == RuntimePlatform.WindowsWebPlayer);
//if (!isWebPlayer)
//{
//if (GUI.Button( Rect( (Screen.width/2)-70, Screen.height - 80, 140, 70),
//"Quit")) Application.Quit();
//}
//if (isLoading)
//{
//GUI.Label ( Rect( (Screen.width/2)-110, (Screen.height / 2) - 60, 400, 70),
//"Loading...", "mainMenuTitle");
//}
}